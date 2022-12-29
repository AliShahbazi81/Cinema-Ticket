using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_Commerce.Data;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace e_Commerce.Services;

public class PaymentService
{
    private readonly IConfiguration _configuration;

    public PaymentService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<PaymentIntent> CreateOrUpdateIntent(Basket basket)
    {
        // Get the API key from the configuration
        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

        // Create a payment intent service
        var service = new PaymentIntentService();

        // Create a payment intent
        var intent = new PaymentIntent();
        // Calculate the total price of the basket
        var subtotal = basket.BasketItems.Sum(item => item.Quantity * item.Product.Price);
        // Calculate the delivery price
        var deliveryFee = subtotal > 10000 ? 0 : 500;

        // !If the PaymentIntentId is null in our database, then we are creating a new payment intent
        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            // Create a new payment intent with the required parameters
            var options = new PaymentIntentCreateOptions
            {
                Amount = subtotal + deliveryFee,
                Currency = "cad",
                PaymentMethodTypes = new List<string> { "card" }
            };
            intent = await service.CreateAsync(options);
        }
        // !If the paymentIntentId is not null, then we are updating our intent
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = subtotal + deliveryFee
            };
            // Update the payment intent
            await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        return intent;
    }
}