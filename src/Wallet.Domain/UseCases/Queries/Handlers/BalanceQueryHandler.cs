using MediatR;
using Wallet.Domain.Helpers.Extensions;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Queries.Requests;
using Wallet.Domain.UseCases.Queries.Responses;
using Wallet.Shared.Handlers;
using Wallet.Shared.Results;

namespace Wallet.Domain.UseCases.Queries.Handlers;

public class BalanceQueryHandler : Handler,
    IRequestHandler<GetBalanceHistoryQuery, ResultData<List<BalanceResponse>>>
{
    private readonly IBalanceRepository _balanceRepository;

    public BalanceQueryHandler(IBalanceRepository balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    public async Task<ResultData<List<BalanceResponse>?>> Handle(GetBalanceHistoryQuery query, CancellationToken cancellationToken)
    {
        var response = new ResultData<List<BalanceResponse>?>();

        if (query.IsInvalid)
            return response.AddNotifications(query);
        
        var balanceHistory = await _balanceRepository.GetAsync(query);
        return Response(balanceHistory.To<List<BalanceResponse>>());
    }
}
