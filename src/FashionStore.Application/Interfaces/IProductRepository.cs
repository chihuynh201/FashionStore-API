using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.QueryModels;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Domain.Entities;

namespace FashionStore.Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<PagedResponse<ProductDto>> GetPagedAsync(ProductQueryModel queryModel);
    Task<ProductDto?> GetByIdWithCategoryAsync(int id);
}
