using Wallet.Shared.Results;

namespace Wallet.Shared.Handlers;

public class Handler
{
    protected static ResultData<T> Response<T>(T data) => new() { Data = data };
}
