using MediatR;
using Wallet.Domain.Helpers.Extensions;
using Wallet.Domain.Interfaces.Repositories.NonRelational;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.Queries.Requests;
using Wallet.Domain.Responses;
using Wallet.Shared.Handlers;
using Wallet.Shared.Others;
using Wallet.Shared.Results;

namespace Wallet.Domain.Queries.Handlers;

public class UserQueryHandler : Handler, 
    IRequestHandler<GetUserByIdQuery, ResultData<UserResponse>>,
    IRequestHandler<GetAllUserQuery, ResultData<PagedResult<UserResponse>>>
{
    private readonly IUserRepository _userRepository;

    public UserQueryHandler(IUserRepository userRepository, IUserReadRepository userReadRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ResultData<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(query.Id);
        return Response(user.To<UserResponse>()!);
    }
    
    public async Task<ResultData<PagedResult<UserResponse>>> Handle(GetAllUserQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAsync(query);
        var usersResponse = users.To<PagedResult<UserResponse>>()!;
        return Response(usersResponse);
    }
}
