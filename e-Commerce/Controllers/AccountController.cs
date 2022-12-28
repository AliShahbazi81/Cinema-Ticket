using System.Linq;
using System.Threading.Tasks;
using e_Commerce.Data;
using e_Commerce.DTOs;
using e_Commerce.Extensions;
using e_Commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Controllers;

public class AccountController : BaseApiController
{
    private readonly ApplicationDbContext _dbContext;
    private readonly TokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager, TokenService tokenService, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        // check if user exists
        var user = await _userManager.FindByNameAsync(loginDto.userName);

        // if user does not exist OR password is incorrect
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.password))
            return Unauthorized();

        // We have 2 kinds of users, Registered users and anonymous users
        // Registered users => card will be sticked to their username 
        // Anonymous users => card will be sticked to their cookies
        var userBasket = await RetrieveBasket(loginDto.userName);
        var anonBasket = await RetrieveBasket(Request.Cookies["buyerId"]);

        if (anonBasket != null)
        {
            if (userBasket != null) _dbContext.Baskets.Remove(userBasket);
            anonBasket.UserId = user.UserName;
            Response.Cookies.Delete("buyerId");
            await _dbContext.SaveChangesAsync();
        }

        // After login, email, token and basket will be returned to the user.
        // Client side should be ready to receive these values.
        return new UserDto
        {
            Email = user.Email,
            Token = await _tokenService.GenerateToken(user),
            Basket = anonBasket != null ? anonBasket.MapBasketToDto() : userBasket?.MapBasketToDto()
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        // check if the username is taken
        if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.userName))
            return BadRequest("Username is taken");

        // check if the email is taken
        if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.email))
            return BadRequest("Email is taken");

        // If not, create a new user
        var user = new User
        {
            //! Since Email and Username will be saved into the database without any changes or hashing, we will save them directly 
            //! BUT, since we are using password hashing, we have to use the UserManager to create the password hash
            Email = registerDto.email,
            UserName = registerDto.userName
        };

        // Add the created user in the database using CreateAsync
        //! This will create the user AND the password hash
        var result = await _userManager.CreateAsync(user, registerDto.password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);
            return ValidationProblem();
        }

        // AddToRoleAsync() method is used to add a user to a role
        await _userManager.AddToRolesAsync(user, new[] { "Member", "Admin" });
        return StatusCode(201);
    }

    //! If user is not logged in, this method will return Unauthorized
    [Authorize]
    [HttpGet("currentUser")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        // After login, email, token and basket will be returned to the user.
        var userBasket = await RetrieveBasket(User.Identity.Name);

        return new UserDto
        {
            Email = user.Email,
            Token = await _tokenService.GenerateToken(user),
            Basket = userBasket?.MapBasketToDto()
        };
    }

    [Authorize]
    [HttpGet("savedAddress")]
    public async Task<ActionResult<UserAddress>> GetSavedAddress()
    {
        return await _userManager.Users
            .Where(x => x.UserName == User.Identity.Name)
            .Select(x => x.Address)
            .FirstOrDefaultAsync();
    }

    // This method will be used to retrieve the basket of the user
    // It is better to separate because it is being used in Account and Basket controller
    private async Task<Basket> RetrieveBasket(string buyerId)
    {
        if (string.IsNullOrEmpty(buyerId))
        {
            Response.Cookies.Delete("buyerId");
            return null;
        }

        var basket = await _dbContext.Baskets
            .Include(i => i.BasketItems)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(x => x.UserId == buyerId);
        return basket;
    }
}