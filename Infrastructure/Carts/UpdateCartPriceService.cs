using Domain._Base.Interfaces;
using Domain.Carts;
using Domain.Carts.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;

namespace Infrastructure.Carts
{
    public class UpdateCartPriceService : BackgroundService
    {
        #region Fields

        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly Channel<(Guid, decimal)> channel;

        #endregion

        #region Constructors

        public UpdateCartPriceService(IServiceScopeFactory serviceScopeFactory, Channel<(Guid, decimal)> channel)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.channel = channel;
        }

        #endregion

        #region Methods

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var (productId, newPrice) in channel.Reader.ReadAllAsync(stoppingToken))
            {
                using IServiceScope scope = serviceScopeFactory.CreateScope();
                IServiceProvider service = scope.ServiceProvider;

                IMediator mediator = service.GetRequiredService<IMediator>();
                ICartRepository cartRepository = service.GetRequiredService<ICartRepository>();
                IUnitOfWork unitOfWork = service.GetRequiredService<IUnitOfWork>();

                List<Cart> carts = await cartRepository.GetByProductId(productId);
                if (carts.Count == 0)
                    return;

                foreach (Cart cart in carts)
                {
                    cart.ChangeItemPrice(productId, newPrice);
                }

                await unitOfWork.SaveAsync(stoppingToken);
            }
        }

        #endregion
    }
}

