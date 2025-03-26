using Domain.Base.Interfaces;

namespace Domain.ProductRates.Interfaces
{
    public interface IProductRateRepository : IGenericRepository<ProductRate, int>
    {
        public Task<ProductRate> GetByProductIdAndUserIdAsync(string userId, Guid productId);
        public Task UpdateProductRates(string userId, Guid productId, decimal rate);
    }
}
