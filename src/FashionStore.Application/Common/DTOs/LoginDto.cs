namespace FashionStore.Application.Common.DTOs;
public class LoginDto
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }

}
