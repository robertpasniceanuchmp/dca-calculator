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

        public async Task<string> GetLatestQuotes()
        {
            var response = await _httpClient.GetAsync("/v1/cryptocurrency/listings/latest");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetHistoricalQuotes(string symbols, long startDate)
        {
            var response = await _httpClient.GetAsync($"/v2/cryptocurrency/quotes/historical?id={symbols}&time_start={startDate}&interval=30d");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
