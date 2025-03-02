using Domain._Base.Interfaces;
using Domain.Carts;
using Domain.Carts.Interfaces;
using Domain.Products.Interfaces;
using MediatR;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Carts.Commands.Update
{
    internal class UpdateItemPriceCommandHandler : IRequestHandler<UpdateItemPriceCommand>
    {
        #region Fields

        private readonly ICartRepository cartRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;

        #endregion

        #region Constructors

        public UpdateItemPriceCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            this.cartRepository = cartRepository;
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
        }

        #endregion

        #region Methods

        public async Task Handle(UpdateItemPriceCommand request, CancellationToken cancellationToken)
        {
            List<Cart> carts = await cartRepository.GetByProductId(request.ProductId);
            if (carts == null) throw new NotFoundException(Localizations.NotFound);

            foreach (var cart in carts)
            {
                cart.ChangeItemPrice(request.ProductId, request.Price);
            }

            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion
    }
}
