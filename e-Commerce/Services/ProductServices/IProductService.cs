using e_Commerce.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_Commerce.Services.ProductServices;

public interface IProductService
{
    Task<List<Product>> GetAllProducts(string orderBy, string searchTrim);
    Task<Product> GetProduct(int id);
}