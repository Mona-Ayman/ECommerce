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
        #region Fields

        private readonly IProductCachingRepository cacheProductRepository;
        private readonly IMapper mapper;

        #endregion

        #region Constructors

        public GetAllProductsQueryHandler(IProductCachingRepository cacheProductRepository, IMapper mapper)
        {
            this.cacheProductRepository = cacheProductRepository;
            this.mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<PaginatedModel<ProductOutput>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            PaginatedModel<Product> products = await cacheProductRepository.GetAll(request.Search, request.MinPrice, request.MaxPrice, request.PageSize, request.PageNumber);

            return mapper.Map<PaginatedModel<ProductOutput>>(products);
        }

        #endregion
    }
}
