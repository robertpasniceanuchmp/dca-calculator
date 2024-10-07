using AutoMapper;
using DcaCalculator.Application.Features.Queries.GetAllCryptocurrencies;
using DcaCalculator.Application.Interfaces;
using DcaCalculator.Application.Interfaces.Repositories;
using DcaCalculator.Domain.Entitties;
using MediatR;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DcaCalculator.Application.Features.Queries.GetDcaResults
{
    public record GetDcaResultsQuery : IRequest<GetDcaResultsDto>
    {
        public GetDcaResultsQuery(DateTime startDate, double investment, string selectedCoins)
        {
            StartDate = startDate;
            Investment = investment;
            SelectedCoins = selectedCoins;
        }

        public DateTime StartDate { get; set; }
        public double Investment { get; set; }
        public string SelectedCoins { get; set; }
    }

    public class GetDcaResultsHandler : IRequestHandler<GetDcaResultsQuery, GetDcaResultsDto>
    {
        private readonly ICoinmarketCapClient _coinmarketCapClient;
        private readonly ICryptocurrencyRepository _cryptocurrencyRepository;

        public GetDcaResultsHandler(ICoinmarketCapClient coinmarketCapClient, ICryptocurrencyRepository cryptocurrencyRepository)
        {
            _coinmarketCapClient = coinmarketCapClient;
            _cryptocurrencyRepository = cryptocurrencyRepository;
        }

        public async Task<GetDcaResultsDto> Handle(GetDcaResultsQuery query, CancellationToken cancellationToken)
        {
            var result = new GetDcaResultsDto();

            result.Table = await CalculateResults(query);
            result.Total = await GetTotalResults(result.Table);

            return result;
        }

        private async Task<TotalResults> GetTotalResults(List<DcaResultsDto> results)
        {
            var result = new TotalResults();
            var groupedResults = results.GroupBy(x => x.InvestedCoin.Symbol);
            foreach (var groupedResult in groupedResults)
            {
                var availableCryptocurrencies = await _cryptocurrencyRepository.GetCryptocurrencyBySymbolAndDateAsync(groupedResult.Key);
                var amount = groupedResult.Sum(x => x.InvestedCoin.Amount);
                var usdPrice = amount * availableCryptocurrencies.Value;
                result.AcumulatedCoins.Add(groupedResult.Key,
                new AcumulatedCoin()
                {
                    Amount = amount,
                    UsdAmount = usdPrice
                });
            }
            var usdValue = result.AcumulatedCoins.Sum(x => x.Value.UsdAmount);
            var investedAmount = results.Sum(x => x.InvestedAmount);
            result.UsdValue = usdValue;
            result.InvestedAmount = investedAmount;
            result.ROI = ((usdValue - investedAmount)/investedAmount) * 100;

            return result;
        }

        private async Task<List<DcaResultsDto>> CalculateResults(GetDcaResultsQuery query)
        {
            var tableResults = new List<DcaResultsDto>();

            var historicalQuotesResponse =
                await _coinmarketCapClient.GetHistoricalQuotes(
            query.SelectedCoins,
                    ((DateTimeOffset)query.StartDate).ToUnixTimeSeconds());
            var historicalQuotes = JsonConvert.DeserializeObject<GetHistoricalQuotesResponse>(historicalQuotesResponse);
            var selectedCoins = query.SelectedCoins.Split(',');
            foreach (var coin in selectedCoins)
            {
                var selectedPeriod = historicalQuotes.Data[coin];
                if (selectedPeriod != null)
                {
                    foreach (var quote in selectedPeriod.Quotes)
                    {
                        var addedRow = new DcaResultsDto();

                        var periodPrice = quote.InnerQuote.USD.Price;
                        if (periodPrice != 0)
                        {
                            var accumulatedAmount = query.Investment / selectedCoins.Length / periodPrice;
                            addedRow.Date = quote.Timestamp;
                            addedRow.InvestedAmount = query.Investment / selectedCoins.Length;
                            addedRow.Price = periodPrice;
                            addedRow.InvestedCoin.Symbol = selectedPeriod.Symbol;
                            addedRow.InvestedCoin.Amount = accumulatedAmount;
                        }

                        tableResults.Add(addedRow);
                    }
                }
            }

            return tableResults;
        }
    }
}
