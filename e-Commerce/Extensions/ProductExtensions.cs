using System.Collections.Generic;
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

    public static IQueryable<Product> Filter(this IQueryable<Product> query, string brand, string type)
    {
        var brandList = new List<string>();
        var typeList = new List<string>();

        // Check if the brand string is null or empty
        if (!string.IsNullOrWhiteSpace(brand))
            // Split the brand string into an array of strings
            // AddRange() adds the elements of the specified collection to the end of the List<T>.
            brandList.AddRange(brand.ToLower().Split(",").ToList());
        
        if (!string.IsNullOrWhiteSpace(type))
            // Split the type string into an array of strings
            typeList.AddRange(type.ToLower().Split(",").ToList());

        // Return the query with the brand and type strings
        query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));
        query = query.Where(p => typeList.Count == 0 || typeList.Contains(p.Type.ToLower()));

        return query;
    }
}