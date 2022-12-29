using System.Collections.Generic;

namespace e_Commerce.DTOs;

public class BasketDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public List<BasketItemsDto> Items { get; set; }
    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }
}