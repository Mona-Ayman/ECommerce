using Domain._Base.Interfaces;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Exceptions;
using Shared.Resources;
using System.Threading.Channels;

namespace Infrastructure.Products
{
    public class UpdateProductRateService : BackgroundService
    {
        #region Fields

        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly Channel<(Guid, decimal?, decimal)> channel;

        #endregion

        #region Constructors

        public UpdateProductRateService(IServiceScopeFactory serviceScopeFactory, Channel<(Guid, decimal?, decimal)> channel)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.channel = channel;
        }

        #endregion

        #region Methods

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var (productId, oldRate, newRate) in channel.Reader.ReadAllAsync(stoppingToken))
            {
                using IServiceScope scope = serviceScopeFactory.CreateScope();
                IServiceProvider service = scope.ServiceProvider;

                IMediator mediator = service.GetRequiredService<IMediator>();
                IProductRepository productRepository = service.GetRequiredService<IProductRepository>();
                IUnitOfWork unitOfWork = service.GetRequiredService<IUnitOfWork>();

                Product product = await productRepository.FindByIdIncludeRates(productId) ?? throw new NotFoundException(Localizations.NotFound);

                if (oldRate != null)
                    product.RemoveRate(oldRate);

                product.AddRate(newRate);

                await unitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}

