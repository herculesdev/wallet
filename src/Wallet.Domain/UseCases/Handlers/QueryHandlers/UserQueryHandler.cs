using MediatR;
using Wallet.Domain.Entities.Base;
using Wallet.Domain.Helpers.Extensions;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Common.Handlers;
using Wallet.Domain.UseCases.Common.Responses;
using Wallet.Domain.UseCases.Queries.Requests;

namespace Wallet.Domain.UseCases.Handlers.QueryHandlers;

public class UserQueryHandler : BaseHandler, 
    IRequestHandler<GetUserByIdQuery, ResponseData<UserResponse>>,
    IRequestHandler<GetAllUserQuery, ResponseData<PagedResult<UserResponse>>>
{
    private readonly IUserRepository _userRepository;
    
    public UserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ResponseData<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByAsync(query.Id);
        return Response(user.To<UserResponse>());
    }
    
    public async Task<ResponseData<PagedResult<UserResponse>>> Handle(GetAllUserQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetByAsync(query);
        var usersResponse = users.To<PagedResult<UserResponse>>();
        return Response(usersResponse);
    }
}
