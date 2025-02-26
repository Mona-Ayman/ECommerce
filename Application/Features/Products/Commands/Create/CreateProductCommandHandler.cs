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
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await GuardAgainstDuplicatedName(request);

            Product product = new(request.Name, request.Description, request.Price);
            productRepository.Add(product);
            await unitOfWork.SaveAsync(cancellationToken);
        }

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
