using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using e_Commerce.Data;
using e_Commerce.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }
    [HttpGet("GetAllProducts")]
    public async Task<List<Product>> GetAllProducts()
    {
        return await _service.GetAllProducts();
    }
    [HttpGet("GetProduct/{id}")]
    public async Task<Product> GetProduct(int id) => await _service.GetProduct(id);
}