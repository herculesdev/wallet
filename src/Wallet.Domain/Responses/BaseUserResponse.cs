namespace Wallet.Domain.Responses;

public class BaseUserResponse
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = String.Empty;
    
    public string Email { get; init; } = string.Empty;
    
    public DateTime BirthDate { get; init; } = DateTime.UtcNow;
    
    public string PhoneNumber { get; init; } = string.Empty;

    public BaseUserResponse() { }

    public BaseUserResponse(Guid id, string fullName, string email, DateTime birthDate, string phoneNumber)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
    }
}
