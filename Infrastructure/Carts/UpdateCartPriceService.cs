using Application.Features.Carts.Commands.Update;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;

namespace Infrastructure.Carts
{
    public class UpdateCartPriceService : BackgroundService, IUpdateCartPriceService
    {
        private static readonly Channel<(Guid ProductId, decimal NewPrice)> _channel =
            Channel.CreateUnbounded<(Guid, decimal)>();
        private readonly IMediator mediator;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public UpdateCartPriceService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task QueuePriceUpdateAsync(Guid productId, decimal newPrice)
        {
            await _channel.Writer.WriteAsync((productId, newPrice));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🚀 UpdateCartPriceService started...");

            await foreach (var (productId, newPrice) in _channel.Reader.ReadAllAsync(stoppingToken))
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    Console.WriteLine($"🔄 Processing price update: ProductId = {productId}, New Price = {newPrice}");

                    await mediator.Send(new UpdateItemPriceCommand(productId, newPrice), stoppingToken);
                }
            }
        }
    }
}

