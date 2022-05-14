using Flunt.Validations;
using MediatR;
using Wallet.Domain.Enumerations;
using Wallet.Domain.Responses;
using Wallet.Domain.ValueObjects;
using Wallet.Shared.Commands;
using Wallet.Shared.Results;

namespace Wallet.Domain.Commands.Requests;

public class CreateUserCommand : Command, IRequest<ResultData<UserResponse?>>
{
    public string Name { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public DateTime BirthDate { get; init; } = DateTime.UtcNow;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Document { get; init; } = string.Empty;
    public LegalNature Nature { get; init; } = LegalNature.PhysicalPerson;

    private bool ComeOfAge => BirthDate <= DateTime.UtcNow.AddYears(-18);

    public CreateUserCommand()
    {

    }

    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsNotNullOrEmpty(Name, nameof(Name), "Nome é obrigatório")
            .IsNotNullOrEmpty(LastName, nameof(LastName), "Sobrenome é obrigatório")
            .IsTrue(ComeOfAge, nameof(BirthDate), "É necessário ser maior de idade")
            .IsNotNullOrEmpty(PhoneNumber, nameof(Email), "Telefone é obrigatório")
            .IsNotNullOrEmpty(Email, nameof(Email), "Email é obrigatório")
            .IsNotNullOrEmpty(Password, nameof(Password), "Senha é obrigatório")
            .IsTrue(new DocumentNumber(Document)
                .IsValidForLegalNature(Nature), nameof(Document), "Número de documento inválido"));
    }

}
