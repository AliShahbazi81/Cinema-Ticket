using Microsoft.AspNetCore.Identity;

namespace e_Commerce.Data;

public class User : IdentityUser<int>
{
    public UserAddress Address { get; set; }
}