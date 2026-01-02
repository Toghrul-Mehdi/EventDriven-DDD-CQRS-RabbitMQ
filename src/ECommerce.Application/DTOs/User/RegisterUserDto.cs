namespace ECommerce.Application.DTOs.User;
public class RegisterUserDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime DateBirth { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public bool AcceptTerms { get; set; }
}
