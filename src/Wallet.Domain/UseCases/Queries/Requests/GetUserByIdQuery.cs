using MediatR;
using Wallet.Domain.UseCases.Common.Queries;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Queries.Requests;

public class GetUserByIdQuery : BaseQuery, IRequest<ResponseData<UserResponse>>
{
    public Guid Id { get; init; }
    
    public GetUserByIdQuery() { }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
}
