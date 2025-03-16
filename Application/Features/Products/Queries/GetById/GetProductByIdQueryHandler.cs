using Application.Features.Products.DTO;
using Application.Services.CachingService;
using AutoMapper;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductOutput>
    {
        #region Fields

        private readonly IProductCachingRepository productCachingRepository;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        #endregion

        #region Constructors

        public GetProductByIdQueryHandler(IProductCachingRepository productCachingRepository, IMapper mapper, ICacheService cacheService)
        {
            this.productCachingRepository = productCachingRepository;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        #endregion

        #region Methods

        public async Task<ProductOutput> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            //List<Product> cachedProducts = cacheService.GetData<List<Product>, CachingCategory>(CachingCategory.Products);
            //Product product;

            //if (cachedProducts != null)
            //    product = cachedProducts.FirstOrDefault(p => p.Id == request.Id);

            Product product = await productCachingRepository.FindByIdAsync(request.Id) ?? throw new NotFoundException(Localizations.NotFound);
            return mapper.Map<ProductOutput>(product);
        }

        #endregion
    }
}
