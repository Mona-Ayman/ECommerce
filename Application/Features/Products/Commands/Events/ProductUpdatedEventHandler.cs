using Application.Services;
using Domain.Products.Events;
using MediatR;

namespace Application.Features.Products.Commands.Events
{
    internal class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
    {
        #region Fields

        private readonly ICacheService cacheService;

        #endregion

        #region Constructors

        public ProductUpdatedEventHandler(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        #endregion

        #region Methods

        public async Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            cacheService.RemoveData(notification.Key);
        }

        #endregion
    }
}
