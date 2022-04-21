using Flunt.Validations;
using MediatR;
using Wallet.Domain.UseCases.Commands.Responses;
using Wallet.Domain.UseCases.Common.Commands;
using Wallet.Domain.UseCases.Common.Responses;
using Wallet.Domain.ValueObjects;

namespace Wallet.Domain.UseCases.Commands.Requests;

public class AuthCommand : BaseCommand, IRequest<ResponseData<AuthResponse>>
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