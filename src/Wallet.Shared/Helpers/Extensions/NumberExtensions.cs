namespace Wallet.Shared.Helpers.Extensions;

public static class NumberExtensions
{
    public static string ToCurrency(this decimal value)
        => $"{value:C}";
    
    public static string ToCurrency(this float value)
        => $"{value:C}";
    
    public static string ToCurrency(this double value)
        => $"{value:C}";
    
    public static string ToCurrency(this int value)
        => $"{value:C}";
}
