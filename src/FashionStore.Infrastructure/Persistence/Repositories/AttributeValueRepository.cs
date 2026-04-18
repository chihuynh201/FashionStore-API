using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;

namespace FashionStore.Infrastructure.Persistence.Repositories;
internal class AttributeValueRepository : Repository<AttributeValue>, IAttributeValueRepository
{
    public AttributeValueRepository(FashionStoreDbContext context) : base(context) { }

}