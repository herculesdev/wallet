using MediatR;
using Wallet.Domain.Responses;
using Wallet.Shared.Queries;
using Wallet.Shared.Results;

namespace Wallet.Domain.Queries.Requests;

public class GetUserByIdQuery : Query, IRequest<ResultData<UserResponse>>
{
    public Guid Id { get; init; }
    
    public GetUserByIdQuery() { }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }

    protected override void Validate()
    {
        
    }
}
