using DcaCalculator.Domain.Common;

namespace DcaCalculator.Domain.Entitties
{
    public class Cryptocurrency : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Value { get; set; }
    }
}
