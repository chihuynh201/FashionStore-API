using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Common.Interfaces.Authentication;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using FashionStore.Domain.Enums;
using Mapster;

namespace FashionStore.Application.Features.Users.Commands.CreateUser;
public record CreateUserCommand : ICommand<int>
{
    public string Username { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public UserRole UserRole { get; init; }
}

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<ActionResponse<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isExistingUser = await _unitOfWork.UserRepository.AnyAsync(x => x.Username == request.Username);
        if (isExistingUser)
        {
            return ActionResponse<int>.CreateResponse(ResponseCode.BadRequest, message: "Username already exists.");
        }

        var user = request.Adapt<User>();
        user.Password = _passwordHasher.HashPassword(request.Password);

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return ActionResponse<int>.Success(user.UserId);
    }
}
