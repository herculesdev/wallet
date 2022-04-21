using MediatR;
using Wallet.Domain.UseCases.Common.Commands;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Commands.Requests;

public class DisapproveUserCommand : ApproveDisapproveUserCommand, IRequest<Response>
{
    public DisapproveUserCommand(Guid userIdToApprove)
    {
        UserId = userIdToApprove;
    }
}