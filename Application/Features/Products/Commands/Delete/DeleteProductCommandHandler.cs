using Domain._Base.Interfaces;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        #region Fields

        private readonly IProductCachingRepository productCachingRepository;
        private readonly IUnitOfWork unitOfWork;

        #endregion

        #region Constructors

        public DeleteProductCommandHandler(IProductCachingRepository productCachingRepository, IUnitOfWork unitOfWork)
        {
            this.productCachingRepository = productCachingRepository;
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await productCachingRepository.FindByIdAsync(request.Id);

            product.Delete();

            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion
    }
}
