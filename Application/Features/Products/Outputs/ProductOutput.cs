using Domain.Products.Enums;

namespace Application.Features.Products.DTO
{
    public record ProductOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] RowVersion { get; set; }
        public ProductState State { get; set; }

        public decimal? AvarageRate { get; set; }
        public int? TotalCountOfUserRates { get; set; }
    }
}