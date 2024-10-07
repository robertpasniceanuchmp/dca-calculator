namespace DcaCalculator.Application.Features.Queries.GetDcaResults
{
    public class GetDcaResultsDto
    {
        public TotalResults Total { get; set; }
        public List<DcaResultsDto> Table { get; set; }
    }

    public class TotalResults
    {
        public TotalResults()
        {
            AcumulatedCoins = new Dictionary<string, AcumulatedCoin>();
        }

        public Dictionary<string, AcumulatedCoin> AcumulatedCoins { get; set; }
        public double UsdValue { get; set; }
        public double InvestedAmount { get; set; }
        public double ROI { get; set; }
    }

    public class AcumulatedCoin
    {
        public double Amount { get; set; }
        public double UsdAmount { get; set; }
    }

    public class DcaResultsDto
    {
        public DcaResultsDto()
        {
            InvestedCoin = new InvestedCoin();
        }
        public DateTime Date { get; set; }
        public double InvestedAmount { get; set; }
        public double Price { get; set; }
        public InvestedCoin InvestedCoin { get; set; }
    }

    public class InvestedCoin
    {
        public string Symbol { get; set; }
        public double Amount { get; set; }
    }
}
