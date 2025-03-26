using Domain._Base.Interfaces;
using Infrastructure.Persistence.Context.ECommerce.Data;
using Infrastructure.Persistence.Extensions;
using MediatR;

namespace Infrastructure.Persistence._Base
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Fields

        private readonly ECommerceContext context;
        private readonly IMediator mediator;

        #endregion

        #region Constructors

        public UnitOfWork(ECommerceContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        #endregion

        #region Methods

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await mediator.DispatchDomainEventsAsync(context);
            await context.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
