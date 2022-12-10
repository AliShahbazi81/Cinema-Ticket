using System.Collections.Generic;
using System.Linq;

namespace e_Commerce.Data
{
    public class Basket
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public List<BasketItem> BasketItems { get; set; } = new();

        public void AddItem(Product product, int quantity)
        {
            // If we do not have the item, then create it using .Add()
            if(BasketItems.All(item => item.ProductId != product.Id))
                BasketItems.Add(new BasketItem{Product = product, Quantity = quantity});

            // If we had the item, add the quantity
            var existingItem = BasketItems.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingItem != null) existingItem.Quantity += quantity;
        }

        public void RemoveItem(int productId, int quantity)
        {
            // Checking if the item is available in the list
            var item = BasketItems.FirstOrDefault(item => item.ProductId == productId);
            if(item == null) return;
            
            // Reduce the number of quantity if it was not 0
            item.Quantity -= quantity;
            // Remove the item if it meets 0
            if(item.Quantity == 0) BasketItems.Remove(item);
        }
    }
    
}
