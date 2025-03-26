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

        private readonly IProductCachingRepository productCachingRepository;
        private readonly IUnitOfWork unitOfWork;

        #endregion

        #region Constructors

        public CreateProductCommandHandler(IProductCachingRepository productCachingRepository, IUnitOfWork unitOfWork)
        {
            this.productCachingRepository = productCachingRepository;
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await GuardAgainstDuplicatedName(request);

            Product product = new(request.Name, request.Description, request.Price);
            productCachingRepository.Add(product);

            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion

        #region Private Methods

        private async Task GuardAgainstDuplicatedName(CreateProductCommand request)
        {
            bool nameExist = await productCachingRepository.NameExist(request.Name);
            if (nameExist)
                throw new ValidationException(Localizations.Exist);
        }

        #endregion
    }
}
