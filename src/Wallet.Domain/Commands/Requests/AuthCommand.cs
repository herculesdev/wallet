using Flunt.Validations;
using MediatR;
using Wallet.Domain.Commands.Responses;
using Wallet.Domain.ValueObjects;
using Wallet.Shared.Commands;
using Wallet.Shared.Results;

namespace Wallet.Domain.Commands.Requests;

public class AuthCommand : Command, IRequest<ResultData<AuthResponse>>
{
    public AuthCommand()
    {
        
    }
    
    public AuthCommand(string document, string password)
    {
        Document = document;
        Password = password;
    }

    public string Document { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    
    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsTrue(new DocumentNumber(Document).IsValid, nameof(Document), "Número de documento inválido")
            .IsNotNullOrWhiteSpace(Password, nameof(Password), "Senha é obrigatória"));
    }
}
