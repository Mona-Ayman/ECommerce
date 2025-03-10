using Domain._Base.Models;

namespace Domain.Products.Interfaces
{
    public interface ICacheProductRepository
    {
        Task<PaginatedModel<Product>> GetAll(string search, int? minPrice, int? maxPrice, int pageSize, int pageNumber);
    }
}
