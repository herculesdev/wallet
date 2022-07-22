namespace Wallet.Domain.Commands.Responses;

public class AuthResponse
{
    public Guid UserId { get; init; }
    public string UserToken { get; init; } = string.Empty;
    
    public AuthResponse()
    {
        
    }
    
    public AuthResponse(Guid userId, string userToken)
    {
        UserId = userId;
        UserToken = userToken;
    }
}
