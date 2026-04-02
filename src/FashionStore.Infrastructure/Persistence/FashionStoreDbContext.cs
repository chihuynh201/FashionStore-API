using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.Infrastructure.Persistence;
public class FashionStoreDbContext : DbContext
{

    public FashionStoreDbContext(DbContextOptions<FashionStoreDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FashionStoreDbContext).Assembly);
    }

}
