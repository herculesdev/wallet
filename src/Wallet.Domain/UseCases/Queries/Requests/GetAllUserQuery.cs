using MediatR;
using Wallet.Domain.Entities.Base;
using Wallet.Domain.UseCases.Common.Queries;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Queries.Requests;

public class GetAllUserQuery : BasePagedQuery, IRequest<ResponseData<PagedResult<UserResponse>>>
{
    private string _searchString = String.Empty;
    public string SearchString
    {
        get => _searchString ??= ""; 
        init => _searchString = value;
    }
}
