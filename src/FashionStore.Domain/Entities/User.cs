using FashionStore.Domain.Enums;

namespace FashionStore.Domain.Entities;
public class User : BaseEntity
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole UserRole { get; set; }
    public bool IsEnabled { get; set; }
    public bool IsDeleted { get; set; }
    private User() { }
    public User(string username, string name, string email, string password, UserRole userType)
    {
        Username = username;
        Name = name;
        Email = email;
        Password = password;
        UserRole = userType;
        IsEnabled = true;
        IsDeleted = false;
    }
}
