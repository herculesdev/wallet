using MediatR;
using Wallet.Domain.Commands.Requests;
using Wallet.Domain.Entities;
using Wallet.Domain.Entities.User;
using Wallet.Domain.Events;
using Wallet.Domain.Helpers.Extensions;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.Responses;
using Wallet.Shared.Handlers;
using Wallet.Shared.Results;

namespace Wallet.Domain.Commands.Handlers;

public class UserCommandHandler : Handler,
    IRequestHandler<CreateUserCommand, ResultData<UserResponse?>>,
    IRequestHandler<ApproveUserCommand, Result>,
    IRequestHandler<DisapproveUserCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;

    public UserCommandHandler(
        IMediator mediator,
        IUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public async Task<ResultData<UserResponse?>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = new ResultData<UserResponse?>();
        
        if(command.IsInvalid)
            return result.AddNotifications(command);

        if (await _userRepository.HasUserWithAsync(command.Document))
            return result.AddNotification(nameof(command.Document), "Número de documento em uso");

        if (await _userRepository.HasUserWithEmailAsync(command.Email))
            return result.AddNotification(nameof(command.Email), "Email em uso");

        var user = command.To<User>()!;

        await _userRepository.AddAsync(user);

        await _mediator.Publish(new CreatedUserEvent(user), cancellationToken);

        return result.With(user.To<UserResponse>());
    }

    public async Task<Result> Handle(ApproveUserCommand command, CancellationToken cancellationToken)
    {
        var result = new Result();
        
        if (command.IsInvalid)
            return result.AddNotifications(command);

        if (!await _userRepository.HasUserWithAsync(command.UserId))
            return result.AddNotification("Usuário informado não foi localizado");

        var user = await _userRepository.GetAsync(command.UserId);

        if (user!.IsApproved)
            return result.AddNotification("Não é possível aprovar um usuário já aprovado");
        
        user.Approve();
        
        if(!user.Accounts.Any())
            user.AddAccount(new Account { Type = user.Nature });
        
        return result;
    }

    public async Task<Result> Handle(DisapproveUserCommand command, CancellationToken cancellationToken)
    {
        var result = new Result();
        
        if (command.IsInvalid)
            return result.AddNotifications(command);

        if (!await _userRepository.HasUserWithAsync(command.UserId))
            return result.AddNotification("Usuário informado não foi localizado");

        var user = await _userRepository.GetAsync(command.UserId);

        if (user!.IsDisapproved)
            return result.AddNotification("Não é possível reprovar um usuário já reprovado");
        
        user.Disapprove();
        return result;
    }
}
