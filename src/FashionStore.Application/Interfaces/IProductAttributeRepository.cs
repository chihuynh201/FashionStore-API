using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Requests;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Domain.Entities;

namespace FashionStore.Application.Interfaces;

public interface IProductAttributeRepository : IRepository<ProductAttribute>
{
    Task<PagedResponse<ProductAttributeDto>> GetPagedAsync(QueryModel queryModel);
    Task<ProductAttribute?> GetByIdWithValuesAsync(int id);
}
