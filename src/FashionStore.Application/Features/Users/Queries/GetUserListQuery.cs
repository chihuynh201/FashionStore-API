using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Requests;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;

namespace FashionStore.Application.Features.Users.Queries;
public record GetUserListQuery(QueryModel QueryModel) : ICommand<PagedResponse<UserDto>>
{
}
public class GetUserListQueryHandler : ICommandHandler<GetUserListQuery, PagedResponse<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetUserListQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ActionResponse<PagedResponse<UserDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var pagedUsers = await _unitOfWork.UserRepository.GetPagedAsync(request.QueryModel);
        return ActionResponse<PagedResponse<UserDto>>.Success(pagedUsers);
    }
}
