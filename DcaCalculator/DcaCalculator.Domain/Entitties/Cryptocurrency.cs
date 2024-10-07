using DcaCalculator.Domain.Common;

namespace DcaCalculator.Domain.Entitties
{
    public class Cryptocurrency : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Value { get; set; }
        public long CoinmarketCapId { get; set; }
    }
}
