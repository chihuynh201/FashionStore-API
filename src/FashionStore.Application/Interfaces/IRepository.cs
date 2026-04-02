using FashionStore.Domain.Entities;
using System.Linq.Expressions;

namespace FashionStore.Application.Interfaces;
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Get element by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Get all elements
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);

    /// <summary>
    /// Get first element 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<T?> GetFirstOrDefaultAsync();
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null);
    /// <summary>
    /// Add new element to database 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Update element in database
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Update(T entity);

    /// <summary>
    /// Delete element from database
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Delete(T entity);
}
