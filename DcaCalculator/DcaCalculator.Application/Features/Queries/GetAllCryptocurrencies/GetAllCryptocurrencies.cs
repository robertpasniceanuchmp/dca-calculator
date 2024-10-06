using AutoMapper;
using AutoMapper.QueryableExtensions;
using DcaCalculator.Application.Interfaces;
using DcaCalculator.Application.Interfaces.Repositories;
using DcaCalculator.Domain.Entitties;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DcaCalculator.Application.Features.Queries.GetAllCryptocurrencies
{
    public record GetAllCryptocurrenciesQuery : IRequest<List<GetAllCryptocurrenciesDto>>;

    public class GetAllCryptocurrenciesQueryHandler : IRequestHandler<GetAllCryptocurrenciesQuery, List<GetAllCryptocurrenciesDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICoinmarketCapClient _coinmarketCapClient;

        public GetAllCryptocurrenciesQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICoinmarketCapClient coinmarketCapClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _coinmarketCapClient = coinmarketCapClient;
        }

        public async Task<List<GetAllCryptocurrenciesDto>> Handle(GetAllCryptocurrenciesQuery query, CancellationToken cancellationToken)
        {
            var availableCryptocurrencies =
             await _unitOfWork.Repository<Cryptocurrency>().Entities
                   .ProjectTo<GetAllCryptocurrenciesDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);

            if (availableCryptocurrencies.Any())
            {
                return availableCryptocurrencies;
            }

            var latestQuotesResponse = await _coinmarketCapClient.GetLatestQuotes();
            var latestQuotes = JsonConvert.DeserializeObject<LatestQuotesResponse>(latestQuotesResponse);
            if (latestQuotes != null && latestQuotes.Data.Any())
            {
                foreach (var item in latestQuotes.Data)
                {
                    item.LastUpdated = DateTime.Now;
                    await _unitOfWork.Repository<Cryptocurrency>().AddAsync(new Cryptocurrency()
                    {
                        Name = item.Name,
                        Symbol = item.Symbol,
                        CreatedDate = item.DateAdded,
                        UpdatedDate = item.LastUpdated,
                        Value = item.Quote.USD.Price
                    });
                }

                availableCryptocurrencies = _mapper.Map<List<GetAllCryptocurrenciesDto>>(latestQuotes.Data);
            }

            return availableCryptocurrencies;
        }
    }
}
