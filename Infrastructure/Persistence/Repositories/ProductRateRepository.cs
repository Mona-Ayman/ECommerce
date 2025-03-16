using Domain.ProductRates;
using Domain.ProductRates.Interfaces;
using Infrastructure.Persistence._Base;
using Infrastructure.Persistence.Context.ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRateRepository : GenericRepository<ProductRate, int>, IProductRateRepository
    {
        #region Constructors

        public ProductRateRepository(ECommerceContext context) : base(context)
        {
        }


        #endregion

        #region Methods

        public async Task<ProductRate> GetByProductIdAndUserIdAsync(string userId, Guid productId)
        {
            return await dbset.FirstOrDefaultAsync(r => r.UserId == userId && r.ProductId == productId);
        }

        public Task UpdateProductRates(string userId, Guid productId, decimal rate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
