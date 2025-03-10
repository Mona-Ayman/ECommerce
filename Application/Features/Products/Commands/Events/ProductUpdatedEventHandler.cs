using Domain.Products.Events;
using Domain.Products.Interfaces;
using MediatR;

namespace Application.Features.Products.Commands.Events
{
    internal class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
    {
        #region Fields

        private readonly ICacheProductRepository cacheProductRepository;

        #endregion

        #region Constructors

        public ProductUpdatedEventHandler(ICacheProductRepository cacheProductRepository)
        {
            this.cacheProductRepository = cacheProductRepository;
        }

        #endregion

        #region Methods

        public async Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            cacheProductRepository.Remove();
        }

        #endregion
    }
}
