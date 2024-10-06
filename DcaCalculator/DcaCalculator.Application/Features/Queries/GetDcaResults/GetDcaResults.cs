using DcaCalculator.Application.Interfaces;
using MediatR;
using System.Globalization;

namespace DcaCalculator.Application.Features.Queries.GetDcaResults
{
    public record GetDcaResultsQuery : IRequest<List<GetDcaResultsDto>>
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

    public class GetDcaResultsHandler : IRequestHandler<GetDcaResultsQuery, List<GetDcaResultsDto>>
    {
        private readonly ICoinmarketCapClient _coinmarketCapClient;

        public GetDcaResultsHandler(ICoinmarketCapClient coinmarketCapClient)
        {
            _coinmarketCapClient = coinmarketCapClient;
        }

        public async Task<List<GetDcaResultsDto>> Handle(GetDcaResultsQuery query, CancellationToken cancellationToken)
        {
            var result = new List<GetDcaResultsDto>();

            var historicalQuotesResponse = 
                await _coinmarketCapClient.GetHistoricalQuotes(
                    query.SelectedCoins, 
                    query.StartDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));

            return await Task.FromResult(result);
        }
    }
}
