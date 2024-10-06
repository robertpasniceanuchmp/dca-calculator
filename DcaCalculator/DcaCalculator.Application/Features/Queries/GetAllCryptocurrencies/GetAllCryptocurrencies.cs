using AutoMapper;
using AutoMapper.QueryableExtensions;
using DcaCalculator.Application.Interfaces;
using DcaCalculator.Application.Interfaces.Repositories;
using DcaCalculator.Domain.Entitties;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

            var latestQuotes = await _coinmarketCapClient.GetLatestQuotes();

            return availableCryptocurrencies;
        }
    }
}
