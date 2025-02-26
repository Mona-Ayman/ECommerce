using Application.Features.Carts.Outputs;
using AutoMapper;
using Domain.Carts;

namespace Application.Features.Carts.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartOutput>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<CartItem, CartItemOutput>();
        }
    }
}
