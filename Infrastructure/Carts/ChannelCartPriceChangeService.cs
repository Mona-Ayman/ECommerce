using Application.Services.ChannelService;
using System.Threading.Channels;

namespace Infrastructure.Carts
{
    public class ChannelCartPriceChangeService : IChannelCartPriceChangeService
    {
        #region Fields

        private readonly Channel<(Guid, decimal)> channel;

        #endregion

        #region Constructors

        public ChannelCartPriceChangeService(Channel<(Guid, decimal)> channel)
        {
            this.channel = channel;
        }

        #endregion

        #region Methods

        public async Task NotifyChannel(Guid productId, decimal newPrice)
        {
            await channel.Writer.WriteAsync((productId, newPrice));
        }

        #endregion
    }
}

