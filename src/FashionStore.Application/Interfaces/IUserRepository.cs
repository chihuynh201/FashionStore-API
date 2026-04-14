using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Requests;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Domain.Entities;

namespace FashionStore.Application.Interfaces;
public interface IUserRepository : IRepository<User>
{
    Task<PagedResponse<UserDto>> GetPagedAsync(QueryModel queryModel);
}
