using Application.Services.CachingService;
using Application.Services.CachingService.Enums;
using Domain._Base.Models;
using Domain.Products;
using Domain.Products.Interfaces;

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
            string key = $"{search}_{minPrice}_{maxPrice}";
            string cachingKey = cacheService.GenerateKey(CachingCategory.Products, key);

            PaginatedModel<Product> products = cacheService.GetData<PaginatedModel<Product>, string>(cachingKey);

            if (products == null)
            {
                products = await productRepository.GetAll(search, minPrice, maxPrice, pageSize, pageNumber);

                DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                cacheService.SetData(cachingKey, products, expirationTime);

                List<string> productCachingKeys = cacheService.AddCachingKeys(CachingCategory.Products, cachingKey);
                cacheService.SetData(CachingCategory.Products, productCachingKeys, expirationTime);
            }

            return products;
        }

        public void Remove()
        {
            cacheService.RemoveData(CachingCategory.Products);
        }
    }
}
