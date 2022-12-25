using System.Threading.Tasks;
using e_Commerce.Data;
using e_Commerce.DTOs;
using e_Commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Controllers;

public class AccountController : BaseApiController
{
    private readonly TokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager, TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        // check if user exists
        var user = await _userManager.FindByNameAsync(loginDto.userName);

        // if user does not exist OR password is incorrect
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.password))
            return Unauthorized();

        return new UserDto
        {
            Email = user.Email,
            Token = await _tokenService.GenerateToken(user)
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

        return new UserDto
        {
            Email = user.Email,
            Token = await _tokenService.GenerateToken(user)
        };
    }
}