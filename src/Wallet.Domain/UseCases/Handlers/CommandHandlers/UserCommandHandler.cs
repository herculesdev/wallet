using MediatR;
using Wallet.Domain.Entities.User;
using Wallet.Domain.Events;
using Wallet.Domain.Helpers.Extensions;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Commands.Requests;
using Wallet.Domain.UseCases.Common.Commands;
using Wallet.Domain.UseCases.Common.Handlers;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Handlers.CommandHandlers;

public class UserCommandHandler : BaseHandler,
    IRequestHandler<CreateUserCommand, ResponseData<UserResponse>>,
    IRequestHandler<ApproveDisapproveUserCommand, Response>
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UserCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<ResponseData<UserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var response = new ResponseData<UserResponse>();
        
        if(command.IsInvalid)
            return response.AddNotifications(command);

        if (await _userRepository.HasUserWith(command.Document))
            return response.AddNotification(nameof(command.Document), "Número de documento em uso");

        if (await _userRepository.HasUserWithEmail(command.Email))
            return response.AddNotification(nameof(command.Email), "Email em uso");
        
        var userEntity = await _userRepository.Add(command.To<User>());
        await _unitOfWork.CommitAsync();
        
        await _mediator.Publish(new CreatedUserEvent(userEntity), cancellationToken);

        return response.With(userEntity.To<UserResponse>());
    }

    public async Task<Response> Handle(ApproveDisapproveUserCommand command, CancellationToken cancellationToken)
    {
        var response = new Response();
        
        if (command.IsInvalid)
            return response.AddNotifications(command);

        if (!await _userRepository.HasUserWith(command.UserId))
            return response.AddNotification("Usuário informado não foi localizado");

        var user = await _userRepository.GetByAsync(command.UserId);

        if (user!.IsApproved)
            return response.AddNotification("Não é possível aprovar um usuário já aprovado");
        
        if(command is ApproveUserCommand)
            user.Approve();
        else
            user.Disapprove();
        
        await _userRepository.Update(user);
        
        return response;
    }
}
