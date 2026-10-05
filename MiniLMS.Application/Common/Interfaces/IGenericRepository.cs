using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Common;

namespace MiniLMS.Application.Common.Interfaces;

public interface IGenericRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false);
    Task<TEntity?> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);

    // spec 

    Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec);
    Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec);
    Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec);


}