using e_Commerce.Data.OrderAggregate;

namespace e_Commerce.DTOs;

public class CreateOrderDto
{
    public bool SaveAddress { get; set; }
    public ShippingAddress ShippingAddress { get; set; }
}