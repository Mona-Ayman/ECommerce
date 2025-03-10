using Application.Features.Products.DTO;
using Application.Services;
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

        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        #endregion

        #region Constructors

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper, ICacheService cacheService)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        #endregion

        #region Methods

        public async Task<ProductOutput> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            List<Product> cachedProducts = cacheService.GetData<List<Product>>(nameof(Product));
            Product product;

            if (cachedProducts != null)
                product = cachedProducts.FirstOrDefault(p => p.Id == request.Id);

            product = await productRepository.FindByIdAsync(request.Id) ?? throw new NotFoundException(Localizations.NotFound);
            return mapper.Map<ProductOutput>(product);
        }

        #endregion
    }
}
