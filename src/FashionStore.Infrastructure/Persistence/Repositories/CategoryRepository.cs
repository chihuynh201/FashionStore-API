using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.Infrastructure.Persistence.Repositories;

internal class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(FashionStoreDbContext context) : base(context)
    {
    }

    public async Task<List<CategoryDto>> GetAllHierarchyAsync(string search = null)
    {
        var sql = @"
            WITH CategoryHierarchy AS (
                SELECT 
                    CategoryId,
                    CategoryName,
                    ParentId,
                    CAST(CAST(CategoryId AS VARCHAR(10)) AS VARCHAR(MAX)) AS Path,
                    0 AS [Level],
                    CAST(RIGHT('0000000000' + CAST(CategoryId AS VARCHAR(10)), 10) AS VARCHAR(MAX)) AS SortPath
                FROM Categories
                WHERE ParentId IS NULL AND IsDeleted = 0

                UNION ALL

                SELECT 
                    c.CategoryId,
                    c.CategoryName,
                    c.ParentId,
                    CAST(h.Path + '/' + CAST(c.CategoryId AS VARCHAR(10)) AS VARCHAR(MAX)),
                    h.[Level] + 1,
                    CAST(h.SortPath + '/' + RIGHT('0000000000' + CAST(c.CategoryId AS VARCHAR(10)), 10) AS VARCHAR(MAX))
                FROM Categories c
                INNER JOIN CategoryHierarchy h ON c.ParentId = h.CategoryId
                WHERE c.IsDeleted = 0
            ),
            DirectProductCounts AS (
                SELECT CategoryId, COUNT(*) AS DirectCount
                FROM Products
                WHERE IsDeleted = 0
                GROUP BY CategoryId
            )
            SELECT ch.CategoryId, ch.CategoryName, ch.ParentId, ch.Path, ch.[Level],
                   ISNULL((
                       SELECT SUM(dpc.DirectCount)
                       FROM CategoryHierarchy sub
                       JOIN DirectProductCounts dpc ON sub.CategoryId = dpc.CategoryId
                       WHERE sub.Path = ch.Path OR sub.Path LIKE ch.Path + '/%'
                   ), 0) AS ProductCount
            FROM CategoryHierarchy ch
            WHERE (@Search IS NULL OR ch.CategoryName LIKE '%' + @Search + '%')
            ORDER BY SortPath";

        var searchParam = new SqlParameter("@Search",
            string.IsNullOrWhiteSpace(search) ? (object)DBNull.Value : search);

        return await _context.Database
            .SqlQueryRaw<CategoryDto>(sql, searchParam)
            .ToListAsync();
    }
}

