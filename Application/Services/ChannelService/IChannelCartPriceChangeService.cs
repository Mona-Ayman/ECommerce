namespace Application.Services.ChannelService
{
    public interface IChannelCartPriceChangeService
    {
        Task NotifyChannel(Guid productId, decimal newPrice);
    }
}
