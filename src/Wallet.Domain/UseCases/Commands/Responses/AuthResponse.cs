namespace Wallet.Domain.UseCases.Commands.Responses;

public class AuthResponse
{
    public AuthResponse()
    {
        
    }
    
    public AuthResponse(Guid userId, string userToken)
    {
        UserId = userId;
        UserToken = userToken;
    }

    public Guid UserId { get; init; }
    public string UserToken { get; init; } = String.Empty;
}
