using DcaCalculator.Application.Interfaces;
using System.Net;

namespace DcaCalculator.Persistence.HttpClients
{
    public class CoinmarketCapClient : ICoinmarketCapClient
    {
        private readonly HttpClient _httpClient;

        public CoinmarketCapClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<dynamic> GetHistoricalQuotes(string symbol, string interval)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetLatestQuotes()
        {
            var response = await _httpClient.GetAsync("/v1/cryptocurrency/listings/latest");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
