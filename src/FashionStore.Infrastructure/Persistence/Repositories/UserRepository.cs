using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;

namespace FashionStore.Infrastructure.Persistence.Repositories;
internal class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(FashionStoreDbContext context) : base(context)
    {
    }

}
