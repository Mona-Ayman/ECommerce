using Domain.Carts.Interfaces;
using Domain.Products.Events;
using MediatR;

namespace Application.Features.Carts.Commands.Events
{
    internal class ProductPriceChangedEventHandler : INotificationHandler<ProductPriceChangedEvent>
    {
        #region Fields

        private readonly ICartRepository cartRepository;

        #endregion

        #region Constructors

        public ProductPriceChangedEventHandler(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        #endregion

        #region Methods

        public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            #region Using SQL Approach

            await cartRepository.UpdatePrice(notification.ProductId, notification.Price);

            #endregion
        }

        #endregion
    }
}
