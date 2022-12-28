using System.Linq;
using e_Commerce.Data.OrderAggregate;
using e_Commerce.DTOs;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Extensions;

public static class OrderExtensions
{
    // The purpose of this extension is to convert the default shape of the order object to a more readable shape
    // Creating this extension is not mandatory but it is highly recommenced
    public static IQueryable<OrderDto> ProjectOrderToOrderDto(this IQueryable<Order> query)
    {
        return query.Select(order => new OrderDto
        {
            Id = order.Id,
            BuyerId = order.BuyerId,
            OrderDate = order.OrderDate,
            ShippingAddress = order.ShippingAddress,
            DeliveryFee = order.DeliveryFee,
            Subtotal = order.Subtotal,
            OrderStatus = order.OrderStatus.ToString(),
            Total = order.GetTotal(),
            OrderItems = order.OrderItems.Select(item => new OrderItemDto
            {
                ProductId = item.ItemOrdered.ProductId,
                Name = item.ItemOrdered.Name,
                PictureUrl = item.ItemOrdered.PictureUrl,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        }).AsNoTracking();
    }
}