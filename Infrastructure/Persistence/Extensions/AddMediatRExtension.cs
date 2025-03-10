using Domain._Base.Models;
using Infrastructure.Persistence.Context.ECommerce.Data;
using MediatR;

namespace Infrastructure.Persistence.Extensions
{
    public static class AddMediatRExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, ECommerceContext context)
        {
            var domainEntries = context.ChangeTracker.Entries<DomainEvents>().Where(e => e.Entity.Events != null && e.Entity.Events.Any());
            List<INotification> domainEvents = domainEntries.SelectMany(e => e.Entity.Events).ToList();
            domainEntries.ToList().ForEach(e => e.Entity.ClearDomainEvents());
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
