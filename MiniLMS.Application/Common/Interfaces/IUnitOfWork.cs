using MiniLMS.Domain.Common;

namespace MiniLMS.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {

        public IGenericRepository<TEntity, Tkey> GetRepo<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
            where Tkey : IEquatable<Tkey>;


        Task<int> SaveChangesAsync();
        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}
