using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using e_Commerce.Data;
using e_Commerce.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce.Controllers;
public class ProductController : BaseApiController
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }
    [HttpGet("GetAllProducts")]
    public async Task<List<Product>> GetAllProducts(string orderBy, string searchTrim) => await _service.GetAllProducts(orderBy, searchTrim);

    [HttpGet("GetProduct/{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _service.GetProduct(id);
        if (product == null)
            return BadRequest(new ProblemDetails { Title = "Product not found!" });
        return product;
    }
}