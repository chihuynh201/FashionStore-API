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

    public async Task<List<CategoryWithAttributesDto>> GetAllCategoryAttributes(string search = null)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(category => string.IsNullOrEmpty(search) || category.CategoryName.Contains(search))
            .OrderBy(category => category.CategoryName)
            .Select(category => new CategoryWithAttributesDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Attributes = category.CategoryAttributes
                    .OrderBy(ca => ca.ProductAttribute.AttributeName)
                    .Select(ca => new CategoryAttributeSummaryDto
                    {
                        CategoryAttributeId = ca.CategoryAttributeId,
                        ProductAttributeId = ca.ProductAttributeId,
                        AttributeName = ca.ProductAttribute.AttributeName
                    })
                    .ToList()
            })
            .ToListAsync();
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
                AttributeName = ca.ProductAttribute.AttributeName,
                AttributeValues = ca.ProductAttribute.AttributeValues
                    .OrderBy(av => av.DisplayOrder)
                    .Select(av => new AttributeValueDto
                    {
                        AttributeValueId = av.AttributeValueId,
                        Value = av.Value,
                        DisplayOrder = av.DisplayOrder
                    })
                    .ToList()
            })
            .ToListAsync();
    }
}
