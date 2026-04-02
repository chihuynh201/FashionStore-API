using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Common.Interfaces.Authentication;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Enums;
using Mapster;
using System.Text.Json.Serialization;

namespace FashionStore.Application.Features.Users.Commands.UpdateUser;
public record UpdateUserCommand() : ICommand
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Username { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public UserRole UserRole { get; init; }
}
public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }
    public async Task<ActionResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "User not found");
        }

        if (await _unitOfWork.UserRepository.AnyAsync(u => u.Username == request.Username && u.UserId != request.Id))
        {
            return ActionResponse.CreateResponse(ResponseCode.BadRequest, message: "Username already exists");
        }

        request.Adapt(user);
        user.Password = _passwordHasher.HashPassword(request.Password);
        await _unitOfWork.SaveChangesAsync();

        return ActionResponse.Success();
    }
}
