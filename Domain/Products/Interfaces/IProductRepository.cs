using Domain.Base.Interfaces;

namespace Domain.Products.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, Guid>
    {
        Task<List<Product>> GetAll(string search, int? minPrice, int? maxPrice, int pageSize, int pageNumber);
        Task<bool> NameExist(string name);
        ValueTask<Product> FindByIdIncludePrice(Guid id);
    }
}
