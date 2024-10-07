using DcaCalculator.Application.Interfaces.Repositories;
using DcaCalculator.Domain.Entitties;
using Microsoft.EntityFrameworkCore;

namespace DcaCalculator.Persistence.Repositories
{
    public class CryptocurrencyRepository : ICryptocurrencyRepository
    {
        private readonly IGenericRepository<Cryptocurrency> _repository;

        public CryptocurrencyRepository(IGenericRepository<Cryptocurrency> repository)
        {
            _repository = repository;
        }

        public async Task<Cryptocurrency> GetCryptocurrencyBySymbolAndDateAsync(string symbol)
        {
            return
                await _repository.Entities
                .FirstOrDefaultAsync(x => x.Symbol == symbol);
        }
    }
}
