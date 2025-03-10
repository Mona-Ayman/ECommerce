namespace Application.Services
{
    public interface IChannelCartPriceChangeService
    {
        Task NotifyChannel(Guid productId, decimal newPrice);
    }
}
