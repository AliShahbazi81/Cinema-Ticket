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
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
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
        if (user.Email != null || user.UserName !=null)
        {
            //! These credentials will be sent to the client using payload
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.UserName),
                new (ClaimTypes.Email, user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            

            // 2. Create a key from the secret key in appsettings.json
            //! This key will be used to encrypt the token
            //! If it is lost, EVERYONE using this token can access to the accounts
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:TokenKey"] ?? string.Empty));

            // 3. Create a credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // 4. Create a token descriptor
            var tokenDescriptor = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        return "User not found";
    }
}