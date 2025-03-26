using Domain._Base.Models;
using Domain.Products;
using Domain.Products.Interfaces;
using Infrastructure.Persistence._Base;
using Infrastructure.Persistence.Context.ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product, Guid>, IProductRepository
    {
        #region Fields

        #endregion

        #region Constructors

        public ProductRepository(ECommerceContext context) : base(context)
        {
        }

        #endregion

        #region Methods

        public async Task<PaginatedModel<Product>> GetAll(string search, int? minPrice, int? maxPrice, int pageSize, int pageNumber)
        {
            IQueryable<Product> productsQuery = dbset.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(search.ToLower()) || p.Description.ToLower().Contains(search.ToLower()));

            if (minPrice.HasValue && maxPrice.HasValue)
                productsQuery = productsQuery.Where(p => p.Price >= minPrice && p.Price <= maxPrice);

            List<Product> products = await productsQuery.ToListAsync();

            int count = products.Count;
            products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            PaginatedModel<Product> paginatedResult = new(pageNumber, pageSize, count, products);
            return paginatedResult;
        }

        public async ValueTask<Product> FindByIdIncludePrice(Guid id)
        {
            return await dbset.Include(p => p.TrackingPrices).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async ValueTask<Product> FindByIdIncludeRates(Guid id)
        {
            return await dbset.Include(p => p.Rates).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> NameExist(string name)
        {
            return await dbset.AnyAsync(p => p.Name == name);
        }

        #endregion
    }
}