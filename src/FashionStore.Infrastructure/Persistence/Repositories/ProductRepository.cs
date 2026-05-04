using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.QueryModels;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using FashionStore.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.Infrastructure.Persistence.Repositories;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(FashionStoreDbContext context) : base(context)
    {
    }

    public async Task<PagedResponse<ProductDto>> GetPagedAsync(ProductQueryModel queryModel)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.File)
            .Where(p => !queryModel.CategoryId.HasValue || p.CategoryId == queryModel.CategoryId)
            .Where(p => !p.IsDeleted)
            .Where(p => string.IsNullOrEmpty(queryModel.KeySearch) || p.ProductName.Contains(queryModel.KeySearch))
            .AsQueryable();

        query = query.ApplyFilter(queryModel.FilterBy, queryModel.FilterValue);

        var totalCount = await query.CountAsync();

        if (!string.IsNullOrWhiteSpace(queryModel.SortBy))
        {
            query = query.OrderByProperty(queryModel.SortBy, queryModel.SortOrder);
        }
        else
        {
            query = query.OrderByDescending(p => p.ProductId);
        }

        var items = await query
            .ApplyPagination(queryModel.GetPagination())
            .Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                Thumbnail = p.File != null ? p.File.Url : null,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.CategoryName,
                IsEnabled = p.IsEnabled
            })
            .ToListAsync();

        return PagedResponse<ProductDto>.Create(items, totalCount, queryModel.GetPagination());
    }

    public async Task<ProductDto?> GetByIdWithCategoryAsync(int id)
    {
        return await _context.Products
            .Where(p => p.ProductId == id && !p.IsDeleted)
            .Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                Thumbnail = p.File != null ? p.File.Url : null,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.CategoryName,
                IsEnabled = p.IsEnabled
            })
            .FirstOrDefaultAsync();
    }
}
