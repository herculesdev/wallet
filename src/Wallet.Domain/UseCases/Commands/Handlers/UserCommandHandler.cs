using MediatR;
using Wallet.Domain.Entities;
using Wallet.Domain.Entities.User;
using Wallet.Domain.Helpers.Extensions;
using Wallet.Domain.Interfaces.Integrations.Messaging;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Commands.Requests;
using Wallet.Domain.UseCases.Common.Responses;
using Wallet.Domain.UseCases.Events;
using Wallet.Shared.Handlers;
using Wallet.Shared.Results;

namespace Wallet.Domain.UseCases.Commands.Handlers;

public class UserCommandHandler : Handler,
    IRequestHandler<CreateUserCommand, ResultData<UserResponse>>,
    IRequestHandler<ApproveUserCommand, Result>,
    IRequestHandler<DisapproveUserCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IQueueHandler _queueHandler;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UserCommandHandler(
        IMediator mediator, 
        IQueueHandler queueHandler,
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository)
    {
        _mediator = mediator;
        _queueHandler = queueHandler;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<ResultData<UserResponse?>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var response = new ResultData<UserResponse?>();
        
        if(command.IsInvalid)
            return response.AddNotifications(command);

        if (await _userRepository.HasUserWithAsync(command.Document))
            return response.AddNotification(nameof(command.Document), "Número de documento em uso");

        if (await _userRepository.HasUserWithEmailAsync(command.Email))
            return response.AddNotification(nameof(command.Email), "Email em uso");

        var user = command.To<User>();

        await _userRepository.AddAsync(user);

        await _mediator.Publish(new CreatedUserEvent(user), cancellationToken);

        return response.With(user.To<UserResponse>());
    }

    public async Task<Result> Handle(ApproveUserCommand command, CancellationToken cancellationToken)
    {
        var response = new Result();
        
        if (command.IsInvalid)
            return response.AddNotifications(command);

        if (!await _userRepository.HasUserWithAsync(command.UserId))
            return response.AddNotification("Usuário informado não foi localizado");

        var user = await _userRepository.GetAsync(command.UserId);

        if (user!.IsApproved)
            return response.AddNotification("Não é possível aprovar um usuário já aprovado");
        
        user.Approve();
        
        if(!user.Accounts.Any())
            user.AddAccount(new Account { Type = user.Nature });
        
        return response;
    }

    public async Task<Result> Handle(DisapproveUserCommand command, CancellationToken cancellationToken)
    {
        var response = new Result();
        
        if (command.IsInvalid)
            return response.AddNotifications(command);

        if (!await _userRepository.HasUserWithAsync(command.UserId))
            return response.AddNotification("Usuário informado não foi localizado");

        var user = await _userRepository.GetAsync(command.UserId);

        if (user!.IsDisapproved)
            return response.AddNotification("Não é possível reprovar um usuário já reprovado");
        
        user.Disapprove();
        return response;
    }
}
