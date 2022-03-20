using Wallet.Domain.ValueObjects;

namespace Wallet.Domain.Entities.User;

public class AdminUser : BaseUser
{
    public AdminUser() { }

    public AdminUser(
        Guid id, 
        string name, 
        string lastName, 
        DateTime birthDate,
        string phoneNumber,
        string email, 
        Password password,
        DateTime createdAt,
        DateTime updatedAt) : base(id, name, lastName, birthDate, phoneNumber, email, password, createdAt, updatedAt)
    {
        
    }
}
