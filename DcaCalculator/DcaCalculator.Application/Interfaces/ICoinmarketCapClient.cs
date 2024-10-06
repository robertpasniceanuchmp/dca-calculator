namespace DcaCalculator.Application.Interfaces
{
    public interface ICoinmarketCapClient
    {
        Task<string> GetHistoricalQuotes(string symbol, string startDate);
        Task<string> GetLatestQuotes();
    }
}
