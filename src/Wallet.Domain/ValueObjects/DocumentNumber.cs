using Flunt.Extensions.Br.Validations;
using Flunt.Validations;
using Wallet.Domain.Enumerations;
using Wallet.Domain.ValueObjects.Base;

namespace Wallet.Domain.ValueObjects;

public class DocumentNumber : BaseValueObject
{
    public string Number { get; init; } = string.Empty;
    public bool IsCpf => new Contract<bool>().IsCpf(Number, "").IsValid;
    public bool IsCnpj => new Contract<bool>().IsCnpj(Number, "").IsValid;
    
    public DocumentNumber() { }
    
    public DocumentNumber(string number)
    {
        Number = number;
    }

    public bool IsValidForLegalNature(LegalNature nature)
    {
        if (IsInvalid)
            return false;
        
        if (nature == LegalNature.PhysicalPerson && IsCnpj)
            return false;

        if (nature == LegalNature.JuridicalPerson && IsCpf)
            return false;

        return true;
    }
    
    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsCpfOrCnpj(Number, nameof(Number), "Número de documento inválido"));
    }
    
    public override string ToString() => Number;
    public static implicit operator DocumentNumber(string number) => new DocumentNumber(number);
}
