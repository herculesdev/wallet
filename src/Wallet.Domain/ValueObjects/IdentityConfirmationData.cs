namespace Wallet.Domain.ValueObjects;

public class IdentityConfirmationData
{
    public string FaceUrl { get; set; } = String.Empty;
    public string DocumentFrontUrl { get; set; } = String.Empty;
    public string DocumentBackUrl { get; set; } = String.Empty;
}
