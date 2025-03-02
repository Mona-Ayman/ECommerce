using Application.Services;
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

        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUpdateCartPriceService updateCartPriceService;

        #endregion

        #region Constructors

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IUpdateCartPriceService updateCartPriceService)
        {
            this.unitOfWork = unitOfWork;
            this.updateCartPriceService = updateCartPriceService;
            this.productRepository = productRepository;
        }

        #endregion

        #region Methods

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await productRepository.FindByIdIncludePrice(request.Id);
            await GuardAgainstDuplicatedName(request.Name, product.Name);

            product.Update(request.Name, request.Description, request.Price);
            productRepository.Update(product);
            await unitOfWork.SaveAsync(cancellationToken);

            #region Using BackgroundService Approach

            await updateCartPriceService.QueuePriceUpdateAsync(request.Id, request.Price);

            #endregion
        }

        private async Task GuardAgainstDuplicatedName(string requestName, string productName)
        {
            bool nameExist = await productRepository.NameExist(requestName);
            if (requestName != productName && nameExist)
                throw new ValidationException(Localizations.Exist);
        }

        #endregion
    }
}
