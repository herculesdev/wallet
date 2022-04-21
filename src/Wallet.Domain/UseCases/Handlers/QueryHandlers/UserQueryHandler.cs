using MediatR;
using Wallet.Domain.Entities.Base;
using Wallet.Domain.Enumerations;
using Wallet.Domain.Helpers.Extensions;
using Wallet.Domain.Interfaces.Integrations.Messaging;
using Wallet.Domain.Interfaces.Repositories.NonRelational;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Common.Handlers;
using Wallet.Domain.UseCases.Common.Responses;
using Wallet.Domain.UseCases.Events;
using Wallet.Domain.UseCases.Queries.Requests;

namespace Wallet.Domain.UseCases.Handlers.QueryHandlers;

public class UserQueryHandler : BaseHandler, 
    IRequestHandler<GetUserByIdQuery, ResponseData<UserResponse>>,
    IRequestHandler<GetAllUserQuery, ResponseData<PagedResult<UserResponse>>>
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
    
    public async Task<ResponseData<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByIdAsync(query.Id);
        
        if(user == null && (user = await _userRepository.GetAsync(query.Id)) != null)
            _queue.Publish(RequestedReplicationEvent.Create(user), QueueExchange.Replication);

        return Response(user.To<UserResponse>());
    }
    
    public async Task<ResponseData<PagedResult<UserResponse>>> Handle(GetAllUserQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAsync(query);
        var usersResponse = users.To<PagedResult<UserResponse>>();
        return Response(usersResponse);
    }
}