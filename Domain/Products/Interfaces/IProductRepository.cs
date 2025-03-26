using Domain._Base.Models;
using Domain.Base.Interfaces;

namespace Domain.Products.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, Guid>
    {
        Task<PaginatedModel<Product>> GetAll(string search, int? minPrice, int? maxPrice, int pageSize, int pageNumber);
        Task<bool> NameExist(string name);
        ValueTask<Product> FindByIdIncludePrice(Guid id);
        ValueTask<Product> FindByIdIncludeRates(Guid id);
    }
}
