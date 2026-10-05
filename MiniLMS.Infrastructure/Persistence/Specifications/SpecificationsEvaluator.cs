using Microsoft.EntityFrameworkCore;
using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Common;

namespace MiniLMS.Infrastructure.Persistence.Specifications;

internal static class SpecificationsEvaluator
{
    // Full query: criteria + includes + ordering + pagination + tracking
    public static IQueryable<TEntity> GetQuery<TEntity, TKey>(
        IQueryable<TEntity> inputQuery,
        ISpecifications<TEntity, TKey> spec)
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        var query = GetCriteriaQuery(inputQuery, spec);

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = spec.ThenIncludes.Aggregate(query, (current, path) => current.Include(path));

        if (spec.OrderBy is not null)
            query = query.OrderBy(spec.OrderBy);
        else if (spec.OrderByDesc is not null)
            query = query.OrderByDescending(spec.OrderByDesc);

        if (spec.IsPaginationEnabled)
            query = query.Skip(spec.Skip).Take(spec.Take);

        if (!spec.IsTracking)
            query = query.AsNoTracking();

        return query;
    }

    // Criteria only: used for Count and Any (no includes / ordering / paging)
    public static IQueryable<TEntity> GetCriteriaQuery<TEntity, TKey>(
        IQueryable<TEntity> inputQuery,
        ISpecifications<TEntity, TKey> spec)
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        var query = inputQuery;

        if (spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        return query;
    }
}