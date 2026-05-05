using FashionStore.Application.Common.DTOs;
using FashionStore.Domain.Entities;

namespace FashionStore.Application.Interfaces;

public interface ICategoryAttributeRepository : IRepository<CategoryAttribute>
{
    Task<List<CategoryAttributeDto>> GetByCategoryIdAsync(int categoryId);
}
