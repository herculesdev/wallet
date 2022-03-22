using MediatR;
using Wallet.Domain.UseCases.Commands.Requests;
using Wallet.Domain.UseCases.Common.Handlers;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Handlers.CommandHandlers;

public class AccountCommandHandler : BaseHandler, IRequestHandler<CreateAccountCommand, Response>
{
    public Task<Response> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
