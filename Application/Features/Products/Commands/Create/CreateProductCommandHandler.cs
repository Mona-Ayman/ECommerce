using Application.Services.CachingService;
using Domain._Base.Interfaces;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        #region Fields

        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICacheService cacheService;

        #endregion

        #region Constructors

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.cacheService = cacheService;
        }

        #endregion

        #region Methods

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await GuardAgainstDuplicatedName(request);

            Product product = new(request.Name, request.Description, request.Price);
            productRepository.Add(product);

            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion

        #region Private Methods

        private async Task GuardAgainstDuplicatedName(CreateProductCommand request)
        {
            bool nameExist = await productRepository.NameExist(request.Name);
            if (nameExist)
                throw new ValidationException(Localizations.Exist);
        }

        #endregion
    }
}
