using MediatR;
using Wallet.Domain.Enumerations;
using Wallet.Domain.Events;
using Wallet.Domain.Helpers.Extensions;
using Wallet.Domain.Interfaces.Integrations.Messaging;
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
    private readonly IUserReadRepository _userReadRepository;
    private IQueueHandler _queue;
    
    public UserQueryHandler(IUserRepository userRepository, IUserReadRepository userReadRepository, IQueueHandler queue)
    {
        _userRepository = userRepository;
        _userReadRepository = userReadRepository;
        _queue = queue;
    }
    
    public async Task<ResultData<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByIdAsync(query.Id);
        
        if(user == null && (user = await _userRepository.GetAsync(query.Id)) != null)
            _queue.Publish(RequestedReplicationEvent.Create(user), QueueExchange.Replication);

        return Response(user.To<UserResponse>()!);
    }
    
    public async Task<ResultData<PagedResult<UserResponse>>> Handle(GetAllUserQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAsync(query);
        var usersResponse = users.To<PagedResult<UserResponse>>()!;
        return Response(usersResponse);
    }
}
