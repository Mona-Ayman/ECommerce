using Domain.Carts;
using Domain.Carts.Interfaces;
using Infrastructure.Persistence._Base;
using Infrastructure.Persistence.Context.ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CartRepository : GenericRepository<Cart, int>, ICartRepository
    {
        #region Constructors

        public CartRepository(ECommerceContext context) : base(context)
        {
        }

        public async Task<List<Cart>> GetByProductId(Guid productId)
        {
            return await dbset.Where(c => c.Items.Any(i => i.ProductId == productId)).ToListAsync();
        }

        #endregion

        #region Methods

        public async Task<Cart> GetByUserId(string id)
        {
            return await dbset.FirstOrDefaultAsync(c => c.UserId == id);
        }

        public async Task UpdatePrice(Guid productId, decimal price)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await context.Set<CartItem>().Where(i => i.ProductId == productId).ExecuteUpdateAsync(setters => setters.SetProperty(ci => ci.UnitPrice, price));
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        #endregion
    }
}