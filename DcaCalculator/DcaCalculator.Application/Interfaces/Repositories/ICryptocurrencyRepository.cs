using DcaCalculator.Domain.Entitties;

namespace DcaCalculator.Application.Interfaces.Repositories
{
    public interface ICryptocurrencyRepository
    {
        Task<List<Cryptocurrency>> GetCryptocurrencyBySymbolAndDateAsync(string symbol, DateTime date);
    }
}
