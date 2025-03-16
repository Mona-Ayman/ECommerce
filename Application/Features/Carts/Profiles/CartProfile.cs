using Application.Features.Carts.Outputs;
using AutoMapper;
using Domain.Carts;

namespace Application.Features.Carts.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartOutput>().ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Items.Sum(i => i.GetTotalPrice())));
            CreateMap<CartItem, CartItemOutput>().ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.GetTotalPrice()));
        }
    }
}
