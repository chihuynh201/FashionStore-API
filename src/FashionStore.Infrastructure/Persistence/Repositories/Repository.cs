using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FashionStore.Infrastructure.Persistence.Repositories;
internal class Repository<T> : IRepository<T> where T : BaseEntity
{
    public readonly FashionStoreDbContext _context;
    public Repository(FashionStoreDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(bool tracking = false)
    {
        if (tracking)
        {
            return await Table.ToListAsync();
        }
        return await Table.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, bool tracking = false)
    {
        if (tracking)
        {
            return await Table.Where(expression).ToListAsync();
        }
        return await Table.AsNoTracking().Where(expression).ToListAsync();
    }

    public async Task<T?> GetFirstOrDefaultAsync(bool tracking = false)
    {
        if (tracking)
        {
            return await Table.FirstOrDefaultAsync();
        }
        return await Table.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool tracking = false)
    {
        if (tracking)
        {
            return await Table.FirstOrDefaultAsync(expression);
        }
        return await Table.AsNoTracking().FirstOrDefaultAsync(expression);
    }


    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await Table.AddRangeAsync(entities);

    }

    public void Update(T entity)
    {
        Table.Update(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null)
    {
        if (expression is null)
        {
            return Table.AnyAsync();
        }
        return Table.AnyAsync(expression);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        Table.RemoveRange(entities);
    }
}
