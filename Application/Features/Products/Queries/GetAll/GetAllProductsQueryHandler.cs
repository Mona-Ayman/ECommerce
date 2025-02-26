using Application.Features.Products.DTO;
using AutoMapper;
using Domain._Base.Models;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedModel<ProductOutput>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<PaginatedModel<ProductOutput>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            PaginatedModel<Product> products = await productRepository.GetAll(request.Search, request.MinPrice, request.MaxPrice, request.PageSize, request.PageNumber);

            return mapper.Map<PaginatedModel<ProductOutput>>(products);
        }
    }
}
