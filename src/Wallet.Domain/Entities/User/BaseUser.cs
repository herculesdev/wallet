using Wallet.Domain.ValueObjects;
using Wallet.Shared.Entities;

namespace Wallet.Domain.Entities.User;

public class BaseUser : Entity
{
    public string Name { get; init; } = String.Empty;
    public string LastName { get; init; } = String.Empty;
    public DateTime BirthDate { get; init; } = DateTime.UtcNow;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = String.Empty;
    public Password Password { get; init; } = new Password("");
    
    public string FullName => $"{Name} {LastName}";
    public string Role { get; set; } = string.Empty;
    
    public BaseUser() { }

    public BaseUser(
        Guid id, 
        string name, 
        string lastName, 
        DateTime birthDate,
        string phoneNumber,
        string email, 
        Password password, 
        DateTime createdAt, 
        DateTime updatedAt)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = Password;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
