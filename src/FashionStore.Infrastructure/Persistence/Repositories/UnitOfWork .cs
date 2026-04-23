using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace FashionStore.Infrastructure.Persistence.Repositories;
internal class UnitOfWork : IUnitOfWork
{
    private readonly FashionStoreDbContext _context;
    private readonly Dictionary<string, object> _repositories = new();
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(FashionStoreDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(_context);
        ProductAttributeRepository = new ProductAttributeRepository(_context);
        AttributeValueRepository = new AttributeValueRepository(_context);
        CategoryRepository = new CategoryRepository(_context);
        ProductRepository = new ProductRepository(_context);
    }

    public IUserRepository UserRepository { get; private set; }
    public IProductAttributeRepository ProductAttributeRepository { get; private set; }

    public IAttributeValueRepository AttributeValueRepository { get; private set; }
    public ICategoryRepository CategoryRepository { get; private set; }
    public IProductRepository ProductRepository { get; private set; }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IRepository<T> Repository<T>() where T : BaseEntity
    {
        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryInstance = new Repository<T>(_context);
            _repositories.Add(type, repositoryInstance);
        }
        return (IRepository<T>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    // Transactions
    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        if (_currentTransaction is not null)
            throw new InvalidOperationException("A transaction is already in progress.");

        _currentTransaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitTransactionAsync(CancellationToken ct = default)
    {
        if (_currentTransaction is null)
            throw new InvalidOperationException("No transaction to commit.");

        try
        {
            await SaveChangesAsync(ct);
            await _currentTransaction.CommitAsync(ct);
        }
        catch
        {
            await RollbackTransactionAsync(ct);
            throw;
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken ct = default)
    {
        if (_currentTransaction is null) return;

        await _currentTransaction.RollbackAsync(ct);
        await _currentTransaction.DisposeAsync();
        _currentTransaction = null;
    }

    public async Task ExecuteInTransactionAsync(Func<Task> operation, CancellationToken ct = default)
    {
        await BeginTransactionAsync(ct);
        try
        {
            await operation();
            await CommitTransactionAsync(ct);
        }
        catch
        {
            await RollbackTransactionAsync(ct);
            throw;
        }
    }

}