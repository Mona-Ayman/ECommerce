using Application.Extensions;
using Application.Features.Carts.Outputs;
using AutoMapper;
using Domain.Carts;
using Domain.Carts.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Carts.Queries.GetById
{
    public class GetUserCartCommandHandler : IRequestHandler<GetUserCartCommand, CartOutput>
    {
        #region Fields

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        #endregion

        #region Constructors

        public GetUserCartCommandHandler(IHttpContextAccessor httpContextAccessor, ICartRepository cartRepository, IMapper mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<CartOutput> Handle(GetUserCartCommand request, CancellationToken cancellationToken)
        {
            string userId = httpContextAccessor.GetUserId();

            Cart cart = await cartRepository.GetByUserId(userId);
            if (cart == null)
                cart = new Cart(userId);

            return mapper.Map<CartOutput>(cart);
        }

        #endregion
    }
}
