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

        public async Task<List<Cryptocurrency>> GetCryptocurrencyBySymbolAndDateAsync(string symbol, DateTime date)
        {
            return 
                await _repository.Entities
                .Where(x => x.Symbol == symbol && x.CreatedDate.Value == date)
                .ToListAsync();
        }
    }
}
