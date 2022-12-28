using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Data.OrderAggregate;

// The purpose of owning entity is to have a single entity that is responsible for the lifecycle of the related entities.
// For instance, in this project, every ORDER should have a single Shipment, hence, we can make Shipment an owning entity.
[Owned]
public class ShippingAddress : Address
{
}