using System.Linq;
using e_Commerce.Data;
using e_Commerce.DTOs;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Extensions;

public static class BasketExtensions
{
    public static BasketDto MapBasketToDto(this Basket basket)
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

    public static IQueryable<Basket> RetrieveBasketWithItems(this IQueryable<Basket> baskets, string buyerId)
    {
        return baskets.Include(basket => basket.BasketItems).ThenInclude(item => item.Product)
            .Where(b => b.UserId == buyerId);
    }
}