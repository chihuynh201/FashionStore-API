using FashionStore.Application.Common.DTOs.Requests;
using FashionStore.Application.Common.Enums;
using System.Linq.Expressions;
using System.Reflection;

namespace FashionStore.Infrastructure.Persistence.Extensions;
public static class QueryableExtensions
{
    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> query, string sortBy, SortOrder sortOrder)
    {
        if (string.IsNullOrWhiteSpace(sortBy))
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = typeof(T).GetProperty(
            sortBy,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null)
            return query;

        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        string methodName = sortOrder == SortOrder.Descending ? "OrderByDescending" : "OrderBy";
        var resultExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.PropertyType },
            query.Expression,
            Expression.Quote(orderByExpression));

        return query.Provider.CreateQuery<T>(resultExpression);
    }

    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, Pagination pagination)
    {
        return query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                    .Take(pagination.PageSize);
    }

    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, string? filterBy, string? filterValue)
    {
        if (string.IsNullOrWhiteSpace(filterBy) || string.IsNullOrWhiteSpace(filterValue))
            return query;

        var property = typeof(T).GetProperty(
            filterBy,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null)
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(parameter, property);
        var constant = Expression.Constant(filterValue);

        Expression body;

        if (property.PropertyType == typeof(string))
        {
            var method = typeof(string).GetMethod(
                "Contains",
                new[] { typeof(string) });

            body = Expression.Call(propertyAccess, method!, constant);
        }
        else
        {
            var convertedValue = Convert.ChangeType(
                filterValue,
                Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);

            var convertedConstant = Expression.Constant(convertedValue);
            body = Expression.Equal(propertyAccess, convertedConstant);
        }

        var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
        return query.Where(predicate);
    }
}
