using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.Users.Entities;

public class AppIdentityUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateBirth { get; set; }
}