namespace e_Commerce.RequestHelpers;

public class MetaData
{
    // The purpose of this class is to provide a way to pass metadata parameters to the API
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    
}