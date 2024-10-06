namespace DcaCalculator.Application.Features.Queries.GetDcaResults
{
    public class GetDcaResultsDto
    {
        public DateTime Date { get; set; }
        public double InvestedAmount { get; set; }
        public Dictionary<string, double> InvestedCoins { get; set; }
    }
}
