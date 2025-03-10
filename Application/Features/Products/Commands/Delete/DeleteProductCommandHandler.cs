using Application.Services.CachingService;
using Domain._Base.Interfaces;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        #region Fields

        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICacheService cacheService;

        #endregion

        #region Constructors

        public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.cacheService = cacheService;
        }

        #endregion

        #region Methods

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await productRepository.FindByIdAsync(request.Id);

            product.Delete();

            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion
    }
}
