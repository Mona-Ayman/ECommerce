namespace Application.Services
{
    public interface IUpdateCartPriceService
    {
        Task QueuePriceUpdateAsync(Guid productId, decimal newPrice);
    }
}
