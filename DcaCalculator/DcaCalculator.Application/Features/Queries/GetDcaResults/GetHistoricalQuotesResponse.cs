using DcaCalculator.Application.Features.Queries.GetAllCryptocurrencies;
using Newtonsoft.Json;

namespace DcaCalculator.Application.Features.Queries.GetDcaResults
{
    public class CryptoData
    {
        [JsonProperty("quotes")]
        public List<Quote> Quotes { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("is_active")]
        public int IsActive { get; set; }

        [JsonProperty("is_fiat")]
        public int IsFiat { get; set; }
    }

    public class Quote
    {
        public DateTime Timestamp { get; set; }

        [JsonProperty("quote")]
        public InnerQuote InnerQuote { get; set; }
    }

    public class InnerQuote
    {
        public USD USD { get; set; }
    }

    public class USD
    {
        public double PercentChange1h { get; set; }
        public double PercentChange24h { get; set; }
        public double PercentChange7d { get; set; }
        public double PercentChange30d { get; set; }
        public double Price { get; set; }
        public double Volume24h { get; set; }
        public double MarketCap { get; set; }
        public int TotalSupply { get; set; }
        public int CirculatingSupply { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class GetHistoricalQuotesResponse
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, CryptoData> Data { get; set; }
    }

    public class Status
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty("error_message")]
        public object ErrorMessage { get; set; }

        [JsonProperty("elapsed")]
        public int Elapsed { get; set; }

        [JsonProperty("credit_count")]
        public int CreditCount { get; set; }

        [JsonProperty("notice")]
        public object Notice { get; set; }
    }


}
