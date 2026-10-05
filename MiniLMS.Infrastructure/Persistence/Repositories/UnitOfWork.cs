using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Domain.Common;

namespace MiniLMS.Infrastructure.Persistence.Repositories
{
    internal class UnitOfWork(AppDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();

        public IGenericRepository<TEntity, TKey> GetRepo<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var entityType = typeof(TEntity);

            if (!_repositories.TryGetValue(entityType, out var repository))
            {
                repository = new GenericRepository<TEntity, TKey>(_dbContext);
                _repositories[entityType] = repository;
            }

            return (IGenericRepository<TEntity, TKey>)repository;
        }

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            await action();

            await transaction.CommitAsync();
        }
    }
}