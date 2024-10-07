using DcaCalculator.Application.Common.Mappings;
using DcaCalculator.Domain.Entitties;

namespace DcaCalculator.Application.Features.Queries.GetAllCryptocurrencies
{
    public class GetAllCryptocurrenciesDto : IMapFrom<Cryptocurrency>
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Value { get; set; }
        public long CoinmarketCapId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
