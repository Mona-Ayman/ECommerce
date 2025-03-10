using Application.Extensions;
using Domain._Base.Interfaces;
using Domain.Carts;
using Domain.Carts.Interfaces;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Carts.Commands.Delete
{
    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand>
    {
        #region Fields

        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        #endregion

        #region Constructors

        public RemoveItemCommandHandler(ICartRepository cartRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        public async Task Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            string userId = httpContextAccessor.GetUserId();

            Product product = await productRepository.FindByIdAsync(request.ProductId) ?? throw new NotFoundException(Localizations.NotFound);
            Cart cart = await cartRepository.GetByUserId(userId);
            cart.RemoveItem(product);

            cartRepository.Update(cart);
            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion
    }
}
