using System.Linq;
using e_Commerce.Data;

namespace e_Commerce.Extensions;

public static class ProductExtensions
{
    public static IQueryable<Product> Sort(this IQueryable<Product> query, string orderBy)
    {
        // !Steps of sorting in this function :
        // 1. Check if the orderBy string is null or empty
        // 2. Split the orderBy string into an array of strings
        // 3. Loop through the array of strings
        
        if(string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(p => p.Name);
        query = orderBy switch
        {
            "price" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };
        return query;
    }

    public static IQueryable<Product> Search(this IQueryable<Product> query, string searchTrim)
    {
        // Check if the search string is null or empty
        if (string.IsNullOrWhiteSpace(searchTrim)) return query;
        
        // Return the query with the search string
        var lowerCaseSearchTrim = searchTrim.ToLower();
        return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTrim));
    }
}