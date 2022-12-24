using System;
using System.Linq;
using System.Threading.Tasks;
using e_Commerce.Data;
using e_Commerce.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public BasketController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            // Get basket using cookies from user browser storage
            var basket = await RetrieveBasket();
            if (basket == null) return NotFound();
            return MapBasketToDto(basket);
        }

        
        [HttpPost]
        public async Task<ActionResult> AddItemToBasket(int productId, int quantity)
        {
            // Get basket || Create basket
            var basket = await RetrieveBasket() ?? CreateNewBasket();
            // Get product
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();
            // Add item
            basket.AddItem(product, quantity);
            
            // Save changes
            var checkSaved = await _context.SaveChangesAsync() > 0;
            return checkSaved ? CreatedAtRoute("GetBasket",MapBasketToDto(basket)) : BadRequest(new ProblemDetails{Title = "Problem saving items to basket"});
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteItemFromBasket(int productId, int quantity)
        {
            // Get basket
            var basket = await RetrieveBasket();
            // reduce or remove item
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();
            basket.RemoveItem(productId, quantity);
            // save changes
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return StatusCode(201);
            return BadRequest(new ProblemDetails { Title = "We could not Reduce/Remove the item" });
        }
        private async Task <Basket> RetrieveBasket()
        {
            var basket = await _context.Baskets
                .Include(i => i.BasketItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.UserId == Request.Cookies["buyerId"]);
            return basket;
        }
        private Basket CreateNewBasket()
        {
            // Generating a new Guid for the cookies
            var buyerId = Guid.NewGuid().ToString();
            // Change the required settings for the cookie
            var cookiesOption = new CookieOptions{ IsEssential = true, Expires = DateTime.Now.AddDays(30), SameSite = SameSiteMode.None, Secure = true};
            // Stick the generated Guid with the cookiesOption 
            Response.Cookies.Append("buyerId", buyerId, cookiesOption);
            // Create the new Basket using generated Guid
            var basket = new Basket { UserId = buyerId };
            // Save the created basket into the database
            _context.Baskets.Add(basket);

            return basket;
        }
        private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                UserId = basket.UserId,
                Items = basket.BasketItems.Select(item => new BasketItemsDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureURL,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }
}
