using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Common.Handlers;

public class BaseHandler
{
    protected static ResponseData<T> Response<T>(T data) => new() { Data = data };
}
