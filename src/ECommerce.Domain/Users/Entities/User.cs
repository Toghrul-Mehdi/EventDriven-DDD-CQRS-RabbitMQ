using ECommerce.SharedKernel.Domain;

namespace ECommerce.Domain.Users.Entities;

public  class User : Entity
{
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Email { get; private set; }
    public DateTime DateBirth { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    private User() { } 

    public User(string name, string surname, string email, DateTime dateBirth, string? id = null)
    {
        Id = id ?? Guid.NewGuid().ToString();
        Name = name;
        Surname = surname;
        Email = email;
        DateBirth = dateBirth;
        IsActive = true;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Deactivate() => IsActive = false;
}
