using Domain._Base.Interfaces;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Products.Commands.UpdateState
{
    public class UpdateStateCommandHandler : IRequestHandler<UpdateStateCommand>
    {
        #region Fields

        private readonly IProductCachingRepository productCachingRepository;
        private readonly IUnitOfWork unitOfWork;

        #endregion

        #region Constructors

        public UpdateStateCommandHandler(IProductCachingRepository productCachingRepository, IUnitOfWork unitOfWork)
        {
            this.productCachingRepository = productCachingRepository;
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            Product product = await productCachingRepository.FindByIdAsync(request.Id) ?? throw new NotFoundException(Localizations.NotFound);

            product.ChangeState(request.State);

            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion
    }
}
