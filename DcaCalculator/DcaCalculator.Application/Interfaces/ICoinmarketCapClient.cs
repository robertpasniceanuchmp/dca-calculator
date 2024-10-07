namespace DcaCalculator.Application.Interfaces
{
    public interface ICoinmarketCapClient
    {
        Task<string> GetHistoricalQuotes(string symbol, long startDate);
        Task<string> GetLatestQuotes();
    }
}
