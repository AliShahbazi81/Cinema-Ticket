using System.Linq;
using System.Threading.Tasks;
using e_Commerce.Data;
using e_Commerce.Extensions;
using e_Commerce.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Controllers;
public class ProductController : BaseApiController
{
    private readonly IDbContextFactory<ApplicationDbContext> _context;

    public ProductController( IDbContextFactory<ApplicationDbContext> context)
    {
        _context = context;
    }
    // REMEMBER! This is a service, not a controller. It does not have access to the Request object.
    // Hence, we have to pass the parameters to the service, using [FromQuery] otherwise,
    // Platform assumes that the ProductParams should be gotten from the body of the request !!!
    [HttpGet]
    public async Task<ActionResult<PagedList<Product>>> GetAllProducts([FromQuery] ProductParams productParams)
    {
        await using var dbContext = await _context.CreateDbContextAsync();
        var query =  dbContext.Products
            .Sort(productParams.OrderBy)
            .Search(productParams.SearchTrim)
            .Filter(productParams.Brands, productParams.Type)
            .AsQueryable();

        var product = await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

        // Response.Headers => Means that the response headers are added to the response.
        // So that client can get the pagination data.
        Response.AddPaginationHeader(product.MetaData);
        return product;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        await using var dbContext = await _context.CreateDbContextAsync();
        var getProduct = await dbContext.Products.FindAsync(id);
        if (getProduct == null)
            return BadRequest(new ProblemDetails { Title = "Product not found!" });
        await dbContext.DisposeAsync();
        return getProduct;
    }

    [HttpGet("filters")]
    // HOW IT WORKS:
    // 1. We get the query from the request
    // 2. We get the brands and types from the database
    // 3. We filter the brands and types based on the query
    // 4. We return the filtered brands and types

    public async Task<IActionResult> GetFilters()
    {
        await using var dbContext = await _context.CreateDbContextAsync();
        // Distinct => Returns distinct elements from a sequence by using the default equality comparer to compare values.
        var brands = await dbContext.Products.Select(x => x.Brand).Distinct().ToListAsync();
        var types = await dbContext.Products.Select(x => x.Type).Distinct().ToListAsync();

        await dbContext.DisposeAsync();
        // new {brands, types} => An anonymous type is a type that has no name.
        return Ok(new { brands, types });
    }
}