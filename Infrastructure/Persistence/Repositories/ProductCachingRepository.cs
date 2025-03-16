using Application.Services.CachingService;
using Application.Services.CachingService.Enums;
using Domain._Base.Models;
using Domain.Products;
using Domain.Products.Interfaces;
using Infrastructure.Persistence.Context.ECommerce.Data;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductCachingRepository : ProductRepository, IProductCachingRepository
    {
        #region Fields

        private readonly ICacheService cacheService;

        #endregion

        #region Constructors

        public ProductCachingRepository(ICacheService cacheService, ECommerceContext context) : base(context)
        {
            this.cacheService = cacheService;
        }

        #endregion

        #region Methods

        public new async Task<PaginatedModel<Product>> GetAll(string search, int? minPrice, int? maxPrice, int pageSize, int pageNumber)
        {
            string key = $"{search}_{minPrice}_{maxPrice}";

            PaginatedModel<Product> products = await cacheService.GetData<PaginatedModel<Product>>(CachingCategory.Products, key);

            if (products == null)
            {
                products = await base.GetAll(search, minPrice, maxPrice, pageSize, pageNumber);

                DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                await cacheService.SetData(CachingCategory.Products, key, products, expirationTime);
            }

            return products;
        }

        #endregion
    }
}
