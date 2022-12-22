namespace e_Commerce.RequestHelpers;

public class PaginationParams
{
    // The purpose of this class is to provide a way to pass pagination parameters to the API
    private const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 9;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}