namespace DcaCalculator.Application.Interfaces
{
    public interface ICoinmarketCapClient
    {
        Task<dynamic> GetHistoricalQuotes(string symbol, string interval);
        Task<string> GetLatestQuotes();
    }
}
