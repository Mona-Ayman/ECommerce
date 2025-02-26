using Domain.Base.Interfaces;

namespace Domain.Carts.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart, int>
    {
        Task<Cart> GetByUserId(string id);
        Task UpdatePrice(Guid productId, decimal price);
    }
}
