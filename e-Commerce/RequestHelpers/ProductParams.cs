namespace e_Commerce.RequestHelpers;

public class ProductParams : PaginationParams
{
    public string OrderBy { get; set; }
    public string SearchTrim { get; set; }
    public string Brands { get; set; }
    public string Type { get; set; }
}