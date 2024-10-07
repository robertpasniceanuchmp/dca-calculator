using DcaCalculator.Domain.Entitties;

namespace DcaCalculator.Application.Interfaces.Repositories
{
    public interface ICryptocurrencyRepository
    {
        Task<Cryptocurrency> GetCryptocurrencyBySymbolAndDateAsync(string symbol);
    }
}
