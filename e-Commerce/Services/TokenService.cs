using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using e_Commerce.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace e_Commerce.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public TokenService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    // Steps to generate JWT token:
    // 1. Create a list of claims
    // 2. Create a key from the secret key in appsettings.json
    // 3. Create a credentials
    // 4. Create a token descriptor
    // 5. Create a token handler
    // 6. Create a token
    // 7. Return the token

    public async Task<string> GenerateToken(User user)
    {
        // 1. Create a list of claims
        //! These credentials will be sent to the client using payload
        var claimsList = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email)
        };

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles) claimsList.Add(new Claim(ClaimTypes.Role, role));


        // 2. Create a key from the secret key in appsettings.json
        //! This key will be used to encrypt the token
        //! If it is lost, EVERYONE using this token can access to the accounts
        // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:TokenKey"]));
        //
        // // 3. Create a credentials
        // var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        //
        // // 4. Create a token descriptor
        // var tokenOptions = new JwtSecurityToken
        // (
        //     null,
        //     "localhost",
        //     claimsList,
        //     expires: DateTime.UtcNow.AddDays(7),
        //     signingCredentials: creds
        // );
        //
        var secretKey = Encoding.UTF8.GetBytes(_configuration["JWTSettings:TokenKey"]); // longer that 16 character
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionKey = Encoding.UTF8.GetBytes(_configuration["JWTSettings:EncryptKey"]); //must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey),
            SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claimsPrincipal = await _signInManager.ClaimsFactory.CreateAsync(user);

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWTSettings:Issuer"],
            Audience = _configuration["JWTSettings:Audience"],
            IssuedAt = DateTime.UtcNow,
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claimsPrincipal.Claims),
            Expires = DateTime.UtcNow.AddDays(7)
        });

        var access_token = tokenHandler.WriteToken(securityToken);
        // return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return access_token;
    }
}