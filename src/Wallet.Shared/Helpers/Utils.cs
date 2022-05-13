using System.Reflection;

namespace Wallet.Shared.Helpers;

public static class Utils
{
    public static Assembly GetAssembly() => typeof(Utils).Assembly;
}
