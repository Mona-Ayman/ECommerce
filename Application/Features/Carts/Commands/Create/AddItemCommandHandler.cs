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

namespace Application.Features.Carts.Commands.Create
{
    public class AddItemCommandHandler : IRequestHandler<AddItemCommand>
    {
        #region Fields

        private readonly ICartRepository cartRepository;
        private readonly IProductCachingRepository productCachingRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        #endregion

        #region Constructors

        public AddItemCommandHandler(ICartRepository cartRepository, IProductCachingRepository productCachingRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.cartRepository = cartRepository;
            this.productCachingRepository = productCachingRepository;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        public async Task Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            string userId = httpContextAccessor.GetUserId();
            Product product = await productCachingRepository.FindByIdAsync(request.ProductId) ?? throw new NotFoundException(Localizations.NotFound);

            Cart cart = await cartRepository.GetByUserId(userId);
            if (cart == null)
            {
                cart = new(userId);
                cartRepository.Add(cart);
            }

            cart.AddItem(product);

            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion
    }
}
