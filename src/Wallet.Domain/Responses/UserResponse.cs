using Wallet.Domain.Enumerations;

namespace Wallet.Domain.Responses;

public class UserResponse : BaseUserResponse
{
    public LegalNature Nature { get; set; }
    public string Document { get; init; } = string.Empty;

    public UserResponse()
    {
        
    }
    
    public UserResponse(Guid id, string fullName, string email, DateTime birthDate, string phoneNumber, LegalNature nature, string document) : base(id, fullName, email, birthDate, phoneNumber)
    {
        Nature = nature;
        Document = document;
    }
}
