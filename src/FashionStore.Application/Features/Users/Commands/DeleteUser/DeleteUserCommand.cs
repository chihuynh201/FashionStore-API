using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;

namespace FashionStore.Application.Features.Users.Commands.DeleteUser;
public record DeleteUserCommand(int id) : ICommand
{
}

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ActionResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.id);
        if (user == null || user.IsDeleted)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "User not found");
        }

        user.IsDeleted = true;
        await _unitOfWork.SaveChangesAsync();
        return ActionResponse.Success();
    }
}