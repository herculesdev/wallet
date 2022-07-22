namespace Wallet.Domain.Enumerations;

public enum RegistrationStatus
{
    UnderReview = 1,
    Approved = 2,
    Disapproved = 3,
}

public static class RegistrationStatusExtensions
{
    public static bool IsUnderReview(this RegistrationStatus status) => status == RegistrationStatus.UnderReview;
    public static bool IsApproved(this RegistrationStatus status) => status == RegistrationStatus.Approved;
    public static bool IsDisapproved(this RegistrationStatus status) => status == RegistrationStatus.Disapproved;
}
