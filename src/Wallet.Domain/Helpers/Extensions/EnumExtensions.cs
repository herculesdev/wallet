using System.ComponentModel;

namespace Wallet.Domain.Helpers.Extensions;

public static class EnumExtensions
{
    public static string? GetValueName(this Enum? enumValue)
    {
        return enumValue == null ? null : Enum.GetName(enumValue.GetType(), enumValue);
    }
    
    public static string? GetDescription(this Enum? enumValue)
    {
        if (enumValue == null)
            return null;
        
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        var attrs = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), true);
        return ((DescriptionAttribute)attrs?[0]!)?.Description;
    }
}