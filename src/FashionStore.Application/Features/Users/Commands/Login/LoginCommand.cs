using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Common.Interfaces.Authentication;
using FashionStore.Application.Interfaces;

namespace FashionStore.Application.Features.Users.Commands.Login;
public record LoginCommand(string Username, string Password) : ICommand<LoginDto>
{
}

public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public LoginCommandHandler(IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ActionResponse<LoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.Username == request.Username && x.IsEnabled == true);

        if (user == null)
        {
            return ActionResponse<LoginDto>.CreateResponse(ResponseCode.Unauthorized, message: "Invalid username or password.");
        }

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            return ActionResponse<LoginDto>.CreateResponse(ResponseCode.Unauthorized, message: "Invalid username or password.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
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
