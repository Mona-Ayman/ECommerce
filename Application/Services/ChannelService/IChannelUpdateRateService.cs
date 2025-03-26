namespace Application.Services.ChannelService
{
    public interface IChannelUpdateRateService
    {
        Task NotifyChannel(Guid productId, decimal? oldRate, decimal newRate);
    }
}
