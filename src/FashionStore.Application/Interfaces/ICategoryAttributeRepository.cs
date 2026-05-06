using FashionStore.Application.Common.DTOs;
using FashionStore.Domain.Entities;

namespace FashionStore.Application.Interfaces;

public interface ICategoryAttributeRepository : IRepository<CategoryAttribute>
{
    Task<List<CategoryWithAttributesDto>> GetAllCategoryAttributes(string search = null);
    Task<List<CategoryAttributeDto>> GetByCategoryIdAsync(int categoryId);
}
