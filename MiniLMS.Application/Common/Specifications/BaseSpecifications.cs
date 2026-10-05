using MiniLMS.Domain.Common;
using System.Linq.Expressions;

namespace MiniLMS.Application.Common.Specifications;

public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    // No criteria = "get all"
    protected BaseSpecifications()
    {
    }

    protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<TEntity, bool>>? Criteria { get; }

    public List<Expression<Func<TEntity, object>>> Includes { get; } = new();
    public List<string> ThenIncludes { get; } = new();

    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }

    public bool IsPaginationEnabled { get; private set; }
    public int Skip { get; private set; }
    public int Take { get; private set; }

    public bool IsTracking { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> include)
        => Includes.Add(include);

    protected void AddThenInclude(string includePath)
        => ThenIncludes.Add(includePath);

    protected void ApplyOrderBy(Expression<Func<TEntity, object>> orderBy)
        => OrderBy = orderBy;

    protected void ApplyOrderByDesc(Expression<Func<TEntity, object>> orderByDesc)
        => OrderByDesc = orderByDesc;

    protected void ApplyPagination(int pageIndex, int pageSize)
    {
        IsPaginationEnabled = true;
        Skip = (pageIndex - 1) * pageSize;
        Take = pageSize;
    }

    protected void EnableTracking()
        => IsTracking = true;
}