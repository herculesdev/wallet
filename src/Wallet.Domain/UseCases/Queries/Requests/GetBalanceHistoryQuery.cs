using Flunt.Validations;
using MediatR;
using Wallet.Domain.UseCases.Queries.Responses;
using Wallet.Shared.Query;
using Wallet.Shared.Results;

namespace Wallet.Domain.UseCases.Queries.Requests;

public class GetBalanceHistoryQuery : Query, IRequest<ResultData<List<BalanceResponse>>>
{
    public DateTime InitialDate { get; init; }
    public DateTime FinalDate { get; init; }

    public GetBalanceHistoryQuery(DateTime initialDate, DateTime finalDate)
    {
        InitialDate = DateTime.SpecifyKind(initialDate, DateTimeKind.Utc);
        FinalDate = DateTime.SpecifyKind(finalDate, DateTimeKind.Utc);
    }
    
    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsLowerOrEqualsThan(InitialDate, FinalDate, nameof(InitialDate),
                "Data inicial deve ser menor ou igual a data final")
            .IsLowerOrEqualsThan((FinalDate - InitialDate).Days, 30, nameof(FinalDate),
                "O intervalo deve ser menor ou igual a 30 dias"));
    }
}
