using MediatR;
using Wallet.Domain.UseCases.Responses;
using Wallet.Shared.Entities;
using Wallet.Shared.Query;
using Wallet.Shared.Results;

namespace Wallet.Domain.UseCases.Queries.Requests;

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
