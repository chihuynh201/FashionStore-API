using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.Infrastructure.Persistence.Repositories;

internal class CategoryAttributeRepository : Repository<CategoryAttribute>, ICategoryAttributeRepository
{
    public CategoryAttributeRepository(FashionStoreDbContext context) : base(context)
    {
    }

    public async Task<List<CategoryAttributeDto>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.CategoryAttributes
            .AsNoTracking()
            .Where(ca => ca.CategoryId == categoryId)
            .OrderBy(ca => ca.ProductAttribute.AttributeName)
            .Select(ca => new CategoryAttributeDto
            {
                CategoryAttributeId = ca.CategoryAttributeId,
                ProductAttributeId = ca.ProductAttributeId,
                AttributeName = ca.ProductAttribute.AttributeName
            })
            .ToListAsync();
    }
}
