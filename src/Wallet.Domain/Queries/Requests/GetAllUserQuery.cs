using MediatR;
using Wallet.Domain.Responses;
using Wallet.Shared.Others;
using Wallet.Shared.Queries;
using Wallet.Shared.Results;

namespace Wallet.Domain.Queries.Requests;

public class GetAllUserQuery : PagedQuery, IRequest<ResultData<PagedResult<UserResponse>>>
{
    private string _searchString = String.Empty;
    public string SearchString
    {
        get => _searchString ??= ""; 
        init => _searchString = value;
    }

    protected override void Validate()
    {
        
    }
}
