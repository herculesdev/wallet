using System.Reflection;

namespace Wallet.Shared.Helpers;

public static class Utils
{
    public static string EncryptionKey { get; } = "U62$6ywF$xzn<3<:";
    public static Assembly GetAssembly() => typeof(Utils).Assembly;
}
