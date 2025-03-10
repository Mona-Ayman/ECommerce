using Domain._Base.Models;

namespace Domain.Base.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<List<TEntity>> GetAllAsync();
        ValueTask<TEntity> FindByIdAsync(TKey id);
        void Add(TEntity entity);
        void Update(TEntity entity);
    }
}
