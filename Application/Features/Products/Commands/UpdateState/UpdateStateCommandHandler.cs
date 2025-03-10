using Application.Services.CachingService;
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

        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICacheService cacheService;

        #endregion

        #region Constructors

        public UpdateStateCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.cacheService = cacheService;
        }

        #endregion

        #region Methods

        public async Task Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            Product product = await productRepository.FindByIdAsync(request.Id);
            if (product == null) throw new NotFoundException(Localizations.NotFound);

            product.ChangeState(request.State);

            cacheService.RemoveData(nameof(Product));

            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion
    }
}
