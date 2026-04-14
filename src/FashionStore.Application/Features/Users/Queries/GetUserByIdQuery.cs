using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using Mapster;

namespace FashionStore.Application.Features.Users.Queries;
public record GetUserByIdQuery(int Id) : ICommand<UserDto>
{
}
public class GetUserByIdQueryHandler : ICommandHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);

        var userDto = user?.Adapt<UserDto>();

        return ActionResponse<UserDto>.Success(userDto);
    }
}
