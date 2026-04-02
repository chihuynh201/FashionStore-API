using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Common.Interfaces.Authentication;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using FashionStore.Domain.Enums;
using Mapster;

namespace FashionStore.Application.Features.Users.Commands.CreateUser;
public class CreateInitialUserCommand : ICommand<LoginDto>
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class CreateInitialUserCommandHandler : ICommandHandler<CreateInitialUserCommand, LoginDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public CreateInitialUserCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ActionResponse<LoginDto>> Handle(CreateInitialUserCommand request, CancellationToken cancellationToken)
    {
        var isExistingUser = await _unitOfWork.UserRepository.AnyAsync();
        if (isExistingUser)
        {
            return ActionResponse<LoginDto>.CreateResponse(ResponseCode.Error, message: "Initial user already exists");
        }

        var user = request.Adapt<User>();
        user.Password = _passwordHasher.HashPassword(request.Password);
        user.UserRole = UserRole.Admin;

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(user, 5);
        var result = new LoginDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Token = token.token,
            Expiration = token.expiration
        };

        return ActionResponse<LoginDto>.Success(result);
    }
}
