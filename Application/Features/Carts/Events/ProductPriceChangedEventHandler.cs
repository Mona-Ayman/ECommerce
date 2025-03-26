using Application.Services.ChannelService;
using Domain.Carts.Interfaces;
using Domain.Products.Events;
using MediatR;

namespace Application.Features.Carts.Events
{
    internal class ProductPriceChangedEventHandler : INotificationHandler<ProductPriceChangedEvent>
    {
        #region Fields

        private readonly ICartRepository cartRepository;
        private readonly IChannelCartPriceChangeService channelCartPriceChange;

        #endregion

        #region Constructors

        public ProductPriceChangedEventHandler(ICartRepository cartRepository, IChannelCartPriceChangeService channelCartPriceChange)
        {
            this.cartRepository = cartRepository;
            this.channelCartPriceChange = channelCartPriceChange;
        }

        #endregion

        #region Methods

        public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            #region Using SQL Approach

            //await cartRepository.UpdatePrice(notification.ProductId, notification.Price);

            #endregion

            #region Using BackgroundService Approach

            await channelCartPriceChange.NotifyChannel(notification.ProductId, notification.Price);

            #endregion
        }

        #endregion
    }
}
