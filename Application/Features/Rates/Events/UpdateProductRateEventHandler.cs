using Domain._Base.Interfaces;
using Domain.ProductRates.Events;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Rates.Events
{
    public class UpdateProductRateEventHandler : INotificationHandler<ProductRatedEvent>
    {
        #region Fields

        private readonly IProductCachingRepository productCachingRepository;
        private readonly IUnitOfWork unitOfWork;

        #endregion

        #region Constructors

        public UpdateProductRateEventHandler(IProductCachingRepository productCachingRepository, IUnitOfWork unitOfWork)
        {
            this.productCachingRepository = productCachingRepository;
            this.unitOfWork = unitOfWork;
        }


        #endregion

        #region Methods

        public async Task Handle(ProductRatedEvent notification, CancellationToken cancellationToken)
        {
            Product product = await productCachingRepository.FindByIdIncludeRates(notification.ProductId) ?? throw new NotFoundException(Localizations.NotFound);

            product.UpdateRate();

            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion

    }
}
