using System.Reflection;

namespace Wallet.Domain.Helpers;

public static class Utils
{
    public static string TokenKey { get; } = "J@NcRfUjXn2r5u8x";

    public static Assembly GetAssembly() => typeof(Utils).Assembly;
}
