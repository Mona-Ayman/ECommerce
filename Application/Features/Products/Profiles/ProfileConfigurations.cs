using Application.Features.Products.DTO;
using AutoMapper;
using Domain._Base.Models;
using Domain.Products;

namespace Application.Features.Products.Profiles
{
    public class ProfileConfigurations : Profile
    {
        public ProfileConfigurations()
        {
            CreateMap<Product, ProductOutput>();
            CreateMap<PaginatedModel<Product>, PaginatedModel<ProductOutput>>();
        }
    }
}
