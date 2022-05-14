using MediatR;
using Wallet.Domain.Helpers.Extensions;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.Queries.Requests;
using Wallet.Domain.Queries.Responses;
using Wallet.Shared.Handlers;
using Wallet.Shared.Results;

namespace Wallet.Domain.Queries.Handlers;

public class BalanceQueryHandler : Handler,
    IRequestHandler<GetBalanceHistoryQuery, ResultData<List<BalanceResponse>>>
{
    private readonly IBalanceRepository _balanceRepository;

    public BalanceQueryHandler(IBalanceRepository balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    public async Task<ResultData<List<BalanceResponse>>> Handle(GetBalanceHistoryQuery query, CancellationToken cancellationToken)
    {
        var result = new ResultData<List<BalanceResponse>>(new List<BalanceResponse>());

        if (query.IsInvalid)
            return result.AddNotifications(query);
        
        var balanceHistory = await _balanceRepository.GetAsync(query);
        return result.With(balanceHistory.To<List<BalanceResponse>>()!);
    }
}
