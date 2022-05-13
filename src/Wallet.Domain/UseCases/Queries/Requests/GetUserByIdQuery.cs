using MediatR;
using Wallet.Domain.UseCases.Common.Responses;
using Wallet.Shared.Query;
using Wallet.Shared.Results;

namespace Wallet.Domain.UseCases.Queries.Requests;

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
