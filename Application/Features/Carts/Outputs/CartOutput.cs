namespace Application.Features.Carts.Outputs
{
    public record CartOutput
    {
        public decimal TotalAmount { get; set; }
        public ICollection<CartItemOutput> Items { get; set; } = new List<CartItemOutput>();
    }
}
