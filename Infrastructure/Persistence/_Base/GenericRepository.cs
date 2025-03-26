using Domain._Base.Models;
using Domain.Base.Interfaces;
using Infrastructure.Persistence.Context.ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence._Base
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Fields

        public readonly ECommerceContext context;
        public DbSet<TEntity> dbset;

        #endregion

        #region Constructors

        public GenericRepository(ECommerceContext context)
        {
            this.context = context;
            dbset = context.Set<TEntity>();
        }

        #endregion

        #region Methods

        public Task<List<TEntity>> GetAllAsync()
        {
            return dbset.ToListAsync();
        }

        public ValueTask<TEntity> FindByIdAsync(TKey id)
        {
            return dbset.FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            dbset.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            dbset.Update(entity);
        }

        #endregion
    }
}
