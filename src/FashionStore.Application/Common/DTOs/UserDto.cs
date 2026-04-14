using FashionStore.Domain.Enums;

namespace FashionStore.Application.Common.DTOs;
public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRole UserRole { get; set; }
    public bool IsEnabled { get; set; }
}
