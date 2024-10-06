using DcaCalculator.Domain.Common.Interfaces;

namespace DcaCalculator.Domain.Common
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
