using Domain._Base.Interfaces;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        #region Fields

        private readonly IProductCachingRepository productCachingRepository;
        private readonly IUnitOfWork unitOfWork;

        #endregion

        #region Constructors

        public UpdateProductCommandHandler(IProductCachingRepository productCachingRepository, IUnitOfWork unitOfWork)
        {
            this.productCachingRepository = productCachingRepository;
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await productCachingRepository.FindByIdIncludePrice(request.Id) ?? throw new NotFoundException(Localizations.NotFound);
            await GuardAgainstDuplicatedName(request.Name, product.Name);

            product.Update(request.Name, request.Description, request.Price);
            productCachingRepository.Update(product);

            await unitOfWork.SaveAsync(cancellationToken);
        }

        private async Task GuardAgainstDuplicatedName(string requestName, string productName)
        {
            bool nameExist = await productCachingRepository.NameExist(requestName);
            if (requestName != productName && nameExist)
                throw new ValidationException(Localizations.Exist);
        }

        #endregion
    }
}