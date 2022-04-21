using MediatR;
using Wallet.Domain.UseCases.Common.Commands;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Commands.Requests;

public class ApproveUserCommand : ApproveDisapproveUserCommand, IRequest<Response>
{
    public ApproveUserCommand(Guid userIdToApprove)
    {
        UserId = userIdToApprove;
    }
}