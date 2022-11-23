using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_Commerce.Data;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContext;

    public ProductService(IDbContextFactory<ApplicationDbContext> applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        await using var dbContext = await _applicationDbContext.CreateDbContextAsync();
        var getAll = await dbContext.Products
            .AsNoTracking()
            .ToListAsync();
        await dbContext.DisposeAsync();
        return getAll;
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