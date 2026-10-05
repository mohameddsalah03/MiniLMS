using MiniLMS.Domain.Common;
using System.Linq.Expressions;

namespace MiniLMS.Application.Common.Specifications;

public interface ISpecifications<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    Expression<Func<TEntity, bool>>? Criteria { get; }

    List<Expression<Func<TEntity, object>>> Includes { get; }
    List<string> ThenIncludes { get; }

    Expression<Func<TEntity, object>>? OrderBy { get; }
    Expression<Func<TEntity, object>>? OrderByDesc { get; }

    bool IsPaginationEnabled { get; }
    int Skip { get; }
    int Take { get; }

    bool IsTracking { get; }

}