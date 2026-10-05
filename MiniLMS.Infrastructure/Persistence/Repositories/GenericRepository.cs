using Microsoft.EntityFrameworkCore;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Common;
using MiniLMS.Infrastructure.Persistence.Specifications;

namespace MiniLMS.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(AppDbContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
             => withTracking ? await _dbContext.Set<TEntity>().ToListAsync()
             : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id)
            => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);


        #region With Spec

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec)
            => await ApplaySpecifications(spec).ToListAsync();
        public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
            => await ApplaySpecifications(spec).FirstOrDefaultAsync();


        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
            => await SpecificationsEvaluator.GetCriteriaQuery(_dbContext.Set<TEntity>(), spec).CountAsync();


        #endregion

        #region Helper Methods

        private IQueryable<TEntity> ApplaySpecifications(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationsEvaluator.GetQuery(_dbContext.Set<TEntity>(), spec);
        }

        #endregion
    }
}
