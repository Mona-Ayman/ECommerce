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

        #endregion

        #region Methods

        public async Task<Cart> GetByUserId(string id)
        {
            return await dbset.FirstOrDefaultAsync(c => c.UserId == id);
        }


        public async Task UpdateTotalPrice(Guid productId, decimal price)
        {
            //await context.Database.ExecuteSqlRawAsync(@"
            //        UPDATE CartItems
            //        SET UnitPrice = {0},
            //            TotalPrice = {0} * Quantity
            //        WHERE ProductId = {1}", price, productId);

            //await context.Database.ExecuteSqlRawAsync(@"
            //    UPDATE Carts
            //    SET TotalAmount = (
            //        SELECT COALESCE(SUM(i.TotalPrice), 0)
            //        FROM CartItems i
            //        WHERE i.CartId = Carts.Id
            //    )
            //    WHERE Id IN (
            //        SELECT DISTINCT C.Id
            //        FROM Carts C
            //        INNER JOIN CartItems I ON C.Id = I.CartId
            //        WHERE I.ProductId = {0}
            //    )", productId);

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    await context.Database.ExecuteSqlRawAsync(@"
                                    UPDATE CartItems
                                    SET UnitPrice = {0},
                                        TotalPrice = {0} * Quantity
                                    WHERE ProductId = {1}", price, productId);

                    await context.Database.ExecuteSqlRawAsync(@"
                                    UPDATE Carts
                                    SET TotalAmount = (
                                        SELECT COALESCE(SUM(i.TotalPrice), 0)
                                        FROM CartItems i
                                        WHERE i.CartId = Carts.Id
                                    )
                                    WHERE Id IN (
                                        SELECT DISTINCT C.Id
                                        FROM Carts C
                                        INNER JOIN CartItems I ON C.Id = I.CartId
                                        WHERE I.ProductId = {0}
                                    )", productId);

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        #endregion
    }
}
