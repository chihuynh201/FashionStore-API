using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Requests;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using FashionStore.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.Infrastructure.Persistence.Repositories;

internal class ProductAttributeRepository : Repository<ProductAttribute>, IProductAttributeRepository
{
    public ProductAttributeRepository(FashionStoreDbContext context) : base(context)
    {
    }

    public async Task<PagedResponse<ProductAttributeDto>> GetPagedAsync(QueryModel queryModel)
    {
        var query = _context.ProductAttributes
            .Where(pa => string.IsNullOrEmpty(queryModel.KeySearch) || pa.AttributeName.Contains(queryModel.KeySearch))
            .AsQueryable();

        query = query.ApplyFilter(queryModel.FilterBy, queryModel.FilterValue);

        var totalCount = await query.CountAsync();

        if (!string.IsNullOrWhiteSpace(queryModel.SortBy))
        {
            query = query.OrderByProperty(queryModel.SortBy, queryModel.SortOrder);
        }
        else
        {
            query = query.OrderByDescending(pa => pa.ProductAttributeId);
        }

        var items = await query
            .ApplyPagination(queryModel.GetPagination())
            .Select(pa => new ProductAttributeDto
            {
                ProductAttributeId = pa.ProductAttributeId,
                AttributeName = pa.AttributeName,
                AttributeValues = pa.AttributeValues.OrderBy(av => av.DisplayOrder)
                .Select(av => new AttributeValueDto
                {
                    AttributeValueId = av.AttributeValueId,
                    Value = av.Value,
                    DisplayOrder = av.DisplayOrder
                }).ToList()
            })
            .ToListAsync();

        return PagedResponse<ProductAttributeDto>.Create(items, totalCount, queryModel.GetPagination());
    }

    public async Task<ProductAttribute?> GetByIdWithValuesAsync(int id)
    {
        return await _context.ProductAttributes
            .Include(pa => pa.AttributeValues)
            .FirstOrDefaultAsync(pa => pa.ProductAttributeId == id);
    }
}
