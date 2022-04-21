namespace Wallet.Domain.UseCases.Common.Queries;

public class BasePagedQuery
{
    private readonly int _page;
    public int Page
    {
        get => _page >= 1 ? _page : 1;
        init => _page = value;
    }
    
    private readonly int _pageSize;

    public int PageSize
    {
        get => _pageSize >= 10 ? _pageSize : 10; 
        init => _pageSize = value;
    }

    public int Skip => (Page - 1) * PageSize;
}