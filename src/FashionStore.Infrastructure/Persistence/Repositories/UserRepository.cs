using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Requests;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using FashionStore.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace FashionStore.Infrastructure.Persistence.Repositories;
internal class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(FashionStoreDbContext context) : base(context)
    {
    }

    public async Task<PagedResponse<UserDto>> GetPagedAsync(QueryModel queryModel)
    {
        var query = _context.Users
            .Where(u => string.IsNullOrEmpty(queryModel.KeySearch) || u.Username.Contains(queryModel.KeySearch))
            .Where(u => u.IsDeleted == false).AsQueryable();

        query = query.ApplyFilter(queryModel.FilterBy, queryModel.FilterValue);

        var totalCount = await query.CountAsync();

        if (!string.IsNullOrWhiteSpace(queryModel.SortBy))
        {
            query = query.OrderByProperty(queryModel.SortBy, queryModel.SortOrder);
        }
        else
        {
            query = query.OrderByDescending(u => u.UserId);
        }

        var items = await query
            .ApplyPagination(queryModel.GetPagination())
            .Select(u => new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                Name = u.Name,
                Email = u.Email,
                UserRole = u.UserRole,
                IsEnabled = u.IsEnabled
            })
            .ToListAsync();

        return PagedResponse<UserDto>.Create(items, totalCount, queryModel.GetPagination());
    }
}
