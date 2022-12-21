using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.RequestHelpers;

public class PagedList<T> : List<T>
{
    // How PagedList works:
    // 1. We get the total number of items in the database
    // 2. We get the current page number
    // 3. We get the page size
    // 4. We calculate the total number of pages
    // 5. We calculate the previous page number
    // 6. We calculate the next page number
    // 7. We create a new PagedList object
    // 8. We add the items to the PagedList object
    // 9. We return the PagedList object
    
    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        // MetaData is a class that contains the metadata of the PagedList object
        MetaData = new MetaData
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            // Ceiling returns the smallest integer greater than or equal to the specified decimal number
            // For example: 18 items, 6 items per page, 3 pages
            TotalPage = (int)System.Math.Ceiling(count / (double)pageSize)
        };

        // AddRange() adds the elements of the specified collection to the end of the List<T>.
        this.AddRange(items);
    }
    public MetaData MetaData { get; set; }

    public static async Task<PagedList<T>> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize)
    {
        // CountAsync() returns the number of elements in a sequence asynchronously
        var count = await query.CountAsync();
        // Skip() returns a specified number of contiguous elements from the start of a sequence
        // For example: 18 items, 6 items per page, 3 pages
        // Page 1: Skip 0 items, take 6 items
        // Page 2: Skip 6 items, take 6 items
        // Page 3: Skip 12 items, take 6 items
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}