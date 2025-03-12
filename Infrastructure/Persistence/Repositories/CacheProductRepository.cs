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

            PaginatedModel<Product> products = cacheService.GetData<PaginatedModel<Product>>(CachingCategory.Products, key);

            if (products == null)
            {
                products = await productRepository.GetAll(search, minPrice, maxPrice, pageSize, pageNumber);

                DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                cacheService.SetData(CachingCategory.Products, key, products, expirationTime);
            }

            return products;
        }

        public void Remove()
        {
            cacheService.RemoveData(CachingCategory.Products);
        }
    }
}
