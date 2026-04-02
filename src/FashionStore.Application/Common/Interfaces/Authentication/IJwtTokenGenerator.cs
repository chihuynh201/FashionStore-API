using FashionStore.Domain.Entities;

namespace FashionStore.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    (string token, DateTime expiration) GenerateToken(User user, int expiryMinutes = 0);
}
