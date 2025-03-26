using Application.Services.ChannelService;
using Domain._Base.Interfaces;
using Domain.ProductRates.Events;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Rates.Events
{
    public class UpdateProductRateEventHandler : INotificationHandler<ProductRatedEvent>
    {
        #region Fields

        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IChannelUpdateRateService channelUpdateRateService;

        #endregion

        #region Constructors

        public UpdateProductRateEventHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IChannelUpdateRateService channelUpdateRateService)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.channelUpdateRateService = channelUpdateRateService;
        }

        #endregion

        #region Methods

        public async Task Handle(ProductRatedEvent notification, CancellationToken cancellationToken)
        {
            #region Using RowVersion

            Product product = await productRepository.FindByIdIncludeRates(notification.ProductId) ?? throw new NotFoundException(Localizations.NotFound);

            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {
                    if (notification.OldRate != null)
                        product.RemoveRate(notification.OldRate);

                    product.AddRate(notification.NewRate);

                    await unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    var entry = ex.Entries.Single();
                    var databaseValues = await entry.GetDatabaseValuesAsync();

                    if (databaseValues == null)
                    {
                        throw new NotFoundException(Localizations.NotFound);
                    }

                    var dbProduct = (Product)databaseValues.ToObject();

                    product.UpdateRowVersion(dbProduct.RowVersion);

                    entry.OriginalValues.SetValues(databaseValues);
                }
            } while (saveFailed);

            #endregion

            #region Using Background Service

            //await channelUpdateRateService.NotifyChannel(notification.ProductId, notification.OldRate, notification.NewRate);

            #endregion
        }

        #endregion
    }
}
