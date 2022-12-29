using System.Threading.Tasks;
using e_Commerce.Data;
using e_Commerce.DTOs;
using e_Commerce.Extensions;
using e_Commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Controllers;

public class PaymentController : BaseApiController
{
    private readonly ApplicationDbContext _context;
    private readonly PaymentService _paymentService;

    public PaymentController(PaymentService paymentService, ApplicationDbContext context)
    {
        _paymentService = paymentService;
        _context = context;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent()
    {
        var basket = await _context.Baskets
            .RetrieveBasketWithItems(User.Identity?.Name)
            .FirstOrDefaultAsync();

        if (basket == null) return NotFound();

        var intent = await _paymentService.CreateOrUpdateIntent(basket);

        if (intent == null) return BadRequest(new ProblemDetails { Title = "Problem creating payment intent" });

        // If we have a payment intent or Client secret, then we will update that, otherwise we will create a new one
        basket.PaymentIntentId = basket.PaymentIntentId ?? intent.Id;
        basket.ClientSecret = basket.ClientSecret ?? intent.ClientSecret;

        _context.Update(basket);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest(new ProblemDetails { Title = "Problem updating basket with payment intent" });

        return basket.MapBasketToDto();
    }
}