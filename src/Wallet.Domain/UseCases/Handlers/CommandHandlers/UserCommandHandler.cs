using MediatR;
using Wallet.Domain.Entities;
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
    IRequestHandler<ApproveUserCommand, Response>,
    IRequestHandler<DisapproveUserCommand, Response>
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

        var user = command.To<User>();

        await _userRepository.Add(user);
        await _unitOfWork.CommitAsync();
        
        await _mediator.Publish(new CreatedUserEvent(user), cancellationToken);

        return response.With(user.To<UserResponse>());
    }

    private async Task<Response> ApproveOrDisapproveAsync(ApproveDisapproveUserCommand command, CancellationToken cancellationToken)
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
        
        user.AddAccount(new Account
        {
            Type = user.Nature
        });
        
        await _userRepository.Update(user);
        await _unitOfWork.CommitAsync();
        
        return response;
    }

    public async Task<Response> Handle(ApproveUserCommand command, CancellationToken cancellationToken)
    {
        return await ApproveOrDisapproveAsync(command, cancellationToken);
    }

    public async Task<Response> Handle(DisapproveUserCommand command, CancellationToken cancellationToken)
    {
        return await ApproveOrDisapproveAsync(command, cancellationToken);
    }
}
