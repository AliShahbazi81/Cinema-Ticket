using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using e_Commerce.Data;
using e_Commerce.Extensions;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContext;

    public ProductService(IDbContextFactory<ApplicationDbContext> applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<List<Product>> GetAllProducts(string orderBy, string searchTrim) {
        await using var dbContext = await _applicationDbContext.CreateDbContextAsync();
        var query =  dbContext.Products
            .Sort(orderBy)
            .Search(searchTrim)
            .AsQueryable();
        return await query.ToListAsync();
    }
    public async Task<Product> GetProduct(int id)
    {
        await using var dbContext = await _applicationDbContext.CreateDbContextAsync();
        var getProduct = await dbContext.Products
            .AsNoTracking()
            .Where(x=> x.Id == id)
            .FirstOrDefaultAsync();
        await dbContext.DisposeAsync();
        return getProduct;
    }
}