using Application.Services;
using Domain._Base.Models;
using Domain.Products;
using Domain.Products.Interfaces;
using Infrastructure.Caching.Enums;

namespace Infrastructure.Persistence.Repositories
{
    public class CacheProductRepository : ICacheProductRepository
    {
        private readonly IProductRepository productRepository;
        private readonly ICacheService cacheService;

        public CacheProductRepository(IProductRepository productRepository, ICacheService cacheService)
        {
            this.productRepository = productRepository;
            this.cacheService = cacheService;
        }

        public async Task<PaginatedModel<Product>> GetAll(string search, int? minPrice, int? maxPrice, int pageSize, int pageNumber)
        {
            string cachingKey = $"Product {search}_{minPrice}_{maxPrice}";
            PaginatedModel<Product> products = cacheService.GetData<PaginatedModel<Product>>(cachingKey);

            if (products == null)
            {
                products = await productRepository.GetAll(search, minPrice, maxPrice, pageSize, pageNumber);

                DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                cacheService.SetData(cachingKey, products, expirationTime);
                cacheService.SetData(CachingCategories.Products.ToString(), cachingKey, expirationTime);
            }

            return products;
        }
    }
}
