using Application.Features.Products.DTO;

namespace Application.Features.Carts.Outputs
{
    public class CartItemOutput
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductOutput Product { get; set; }
    }
}
