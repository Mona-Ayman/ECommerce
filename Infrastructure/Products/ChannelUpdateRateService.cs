using Application.Services.ChannelService;
using System.Threading.Channels;

namespace Infrastructure.Products
{
    public class ChannelUpdateRateService : IChannelUpdateRateService
    {
        #region Fields

        private readonly Channel<(Guid, decimal?, decimal)> channel;

        #endregion

        #region Constructors

        public ChannelUpdateRateService(Channel<(Guid, decimal?, decimal)> channel)
        {
            this.channel = channel;
        }

        #endregion

        #region Methods

        public async Task NotifyChannel(Guid productId, decimal? oldRate, decimal newRate)
        {
            await channel.Writer.WriteAsync((productId, oldRate, newRate));
        }

        #endregion
    }
}
