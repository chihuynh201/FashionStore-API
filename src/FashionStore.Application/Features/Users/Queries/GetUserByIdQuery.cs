using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;

namespace FashionStore.Application.Features.Users.Queries;
public class GetUserByIdQuery : ICommand<UserDto>
{
}
public class GetUserByIdQueryHandler : ICommandHandler<GetUserByIdQuery, UserDto>
{
    public Task<ActionResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
