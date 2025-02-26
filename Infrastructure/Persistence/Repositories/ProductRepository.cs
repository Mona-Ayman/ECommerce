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
        #region Constructors

        public ProductRepository(ECommerceContext context) : base(context)
        {
        }

        public async ValueTask<Product> FindByIdIncludePrice(Guid id)
        {
            return await dbset.Include(p => p.TrackingPrices).SingleOrDefaultAsync(p => p.Id == id);
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

            int count = await productsQuery.CountAsync();

            productsQuery = productsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            List<Product> products = await productsQuery.ToListAsync();
            PaginatedModel<Product> paginatedResult = new(pageNumber, pageSize, count, products);

            return paginatedResult;
        }

        public Task<bool> NameExist(string name)
        {
            return dbset.AnyAsync(p => p.Name == name);
        }

        #endregion
    }
}
