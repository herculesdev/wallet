using Wallet.Domain.Enumerations;
using Wallet.Domain.ValueObjects;

namespace Wallet.Domain.Entities.User;

public class User : BaseUser
{
    public DocumentNumber Document { get; init; } = new("");
    public LegalNature Nature { get; init; } = LegalNature.PhysicalPerson;
    public RegistrationStatus RegistrationStatus { get; set; } = RegistrationStatus.UnderReview;
    public DateTime RegistrationStatusUpdatedAt { get; set; } = DateTime.MinValue;
    public string Role { get; set; } = string.Empty;
    
    public bool IsUnderReview => RegistrationStatus == RegistrationStatus.UnderReview;
    public bool IsApproved => RegistrationStatus == RegistrationStatus.Approved;
    public bool IsDisapproved => RegistrationStatus == RegistrationStatus.Disapproved;
    
    public User(string role)
    {
        Role = role;
    }

    public User()
    {
        
    }

    public User(
        Guid id, 
        string name, 
        string lastName, 
        DateTime birthDate,
        string phoneNumber,
        string email, 
        Password password, 
        DocumentNumber document,
        LegalNature nature,
        RegistrationStatus registrationStatus,
        DateTime registrationStatusUpdatedAt,
        string role,
        DateTime createdAt,
        DateTime updatedAt) : base(id, name, lastName, birthDate, phoneNumber, email, password, createdAt, updatedAt)
    {
        Document = document;
        Nature = nature;
        RegistrationStatus = registrationStatus;
        RegistrationStatusUpdatedAt = registrationStatusUpdatedAt;
        Role = role;
    }

    public void PutUnderReview()
    {
        RegistrationStatus = RegistrationStatus.UnderReview;
        RegistrationStatusUpdatedAt = DateTime.UtcNow;
    }
    
    public void Approve()
    {
        RegistrationStatus = RegistrationStatus.Approved;
        RegistrationStatusUpdatedAt = DateTime.UtcNow;
    }
    
    public void Disapprove()
    {
        RegistrationStatus = RegistrationStatus.Disapproved;
        RegistrationStatusUpdatedAt = DateTime.UtcNow;
    }
}
