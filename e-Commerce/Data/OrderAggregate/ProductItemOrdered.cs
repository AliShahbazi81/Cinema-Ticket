using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Data.OrderAggregate;

//! The purpose of this class is to store a copy of user's order details in the database.
// This is done to prevent the user from changing the order details after the order has been placed.
// AND if we decide to change the name of  a product, the history of order (which will be stored in the database) will not be affected.
[Owned]
public class ProductItemOrdered
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
}