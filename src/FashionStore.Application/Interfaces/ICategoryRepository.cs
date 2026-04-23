using FashionStore.Application.Common.DTOs;
using FashionStore.Domain.Entities;

namespace FashionStore.Application.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<CategoryDto>> GetAllHierarchyAsync(string search = null);
}
