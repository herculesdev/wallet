using System.Runtime.Serialization;

namespace Wallet.Domain.Responses;

public class BaseUserResponse
{
    [DataMember(Order = 1)] 
    public Guid Id { get; init; }
    [DataMember(Order = 2)] 
    public string FullName { get; init; } = String.Empty;
    
    [DataMember(Order = 3)] 
    public string Email { get; init; } = string.Empty;
    
    [DataMember(Order = 4)]
    public DateTime BirthDate { get; init; } = DateTime.UtcNow;
    
    [DataMember(Order = 5)]
    public string PhoneNumber { get; init; } = string.Empty;

    public BaseUserResponse() { }

    public BaseUserResponse(Guid id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }
}
