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
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await productRepository.FindByIdIncludePrice(request.Id);
            await GuardAgainstDuplicatedName(request.Name, product.Name);

            product.Update(request.Name, request.Description, request.Price);
            productRepository.Update(product);
            await unitOfWork.SaveAsync(cancellationToken);
        }

        private async Task GuardAgainstDuplicatedName(string requestName, string productName)
        {
            bool nameExist = await productRepository.NameExist(requestName);
            if (requestName != productName && nameExist)
                throw new ValidationException(Localizations.Exist);
        }
    }
}
