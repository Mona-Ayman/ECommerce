using Application.Services.CachingService;
using Application.Services.CachingService.Enums;
using Domain.Products.Events;
using MediatR;

namespace Application.Features.Products.Events
{
    internal class ProductCachingRemovedEventHandler : INotificationHandler<ProductUpdatedEvent>
    {
        #region Fields

        private readonly ICacheService cacheService;

        #endregion

        #region Constructors

        public ProductCachingRemovedEventHandler(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        #endregion

        #region Methods

        public async Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveData(CachingCategory.Products);
        }

        #endregion
    }
}
