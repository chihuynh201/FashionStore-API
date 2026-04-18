using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;

namespace FashionStore.Infrastructure.Persistence.Repositories;
internal class UnitOfWork : IUnitOfWork
{
    private readonly FashionStoreDbContext _context;
    private readonly Dictionary<string, object> _repositories = new();

    public UnitOfWork(FashionStoreDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(_context);
        ProductAttributeRepository = new ProductAttributeRepository(_context);
        AttributeValueRepository = new AttributeValueRepository(_context);
    }

    public IUserRepository UserRepository { get; private set; }
    public IProductAttributeRepository ProductAttributeRepository { get; private set; }

    public IAttributeValueRepository AttributeValueRepository { get; private set; }

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
}
