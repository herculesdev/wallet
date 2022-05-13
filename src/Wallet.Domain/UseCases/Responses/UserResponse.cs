using Wallet.Domain.Enumerations;

namespace Wallet.Domain.UseCases.Responses;

public class UserResponse : BaseUserResponse
{
    public LegalNature Nature { get; set; }
    public string Document { get; init; } = string.Empty;
}
