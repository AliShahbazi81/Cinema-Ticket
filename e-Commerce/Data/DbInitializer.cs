using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace e_Commerce.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext dbContext, UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                UserName = "Ali",
                Email = "Ali@test.com"
            };

            // Since we are using AspNetCore.Identity, the password should be at least 6 characters long
            // and contain at least one digit, one upper case letter and one lower case letter.
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Member");

            var admin = new User
            {
                UserName = "Admin",
                Email = "Admin@test.com"
            };

            // Since we are using AspNetCore.Identity, the password should be at least 6 characters long
            // and contain at least one digit, one upper case letter and one lower case letter.
            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Member" });
        }

        if (dbContext.Products.Any()) return;

        var products = new List<Product>
        {
            new Product
            {
                Name = "Angular Speedster Board 2000",
                Description =
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 20000,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "Angular",
                Type = "Boards",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Green Angular Board 3000",
                Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                Price = 15000,
                PictureURL = "/images/products/sb-ang2.png",
                Brand = "Angular",
                Type = "Boards",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Core Board Speed Rush 3",
                Description =
                    "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                Price = 18000,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "NetCore",
                Type = "Boards",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Net Core Super Board",
                Description =
                    "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                Price = 30000,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "NetCore",
                Type = "Boards",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "React Board Super Whizzy Fast",
                Description =
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 25000,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "React",
                Type = "Boards",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Typescript Entry Board",
                Description =
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 12000,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "TypeScript",
                Type = "Boards",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Core Blue Hat",
                Description =
                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 1000,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "NetCore",
                Type = "Hats",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Green React Woolen Hat",
                Description =
                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 8000,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "React",
                Type = "Hats",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Purple React Woolen Hat",
                Description =
                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 1500,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "React",
                Type = "Hats",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Blue Code Gloves",
                Description =
                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 1800,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "VS Code",
                Type = "Gloves",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Green Code Gloves",
                Description =
                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 1500,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "VS Code",
                Type = "Gloves",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Purple React Gloves",
                Description =
                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 1600,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "React",
                Type = "Gloves",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Green React Gloves",
                Description =
                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 1400,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "React",
                Type = "Gloves",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Redis Red Boots",
                Description =
                    "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                Price = 25000,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "Redis",
                Type = "Boots",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Core Red Boots",
                Description =
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 18999,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "NetCore",
                Type = "Boots",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Core Purple Boots",
                Description =
                    "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                Price = 19999,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "NetCore",
                Type = "Boots",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Angular Purple Boots",
                Description = "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.",
                Price = 15000,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "Angular",
                Type = "Boots",
                QuantityInStock = 100
            },
            new Product
            {
                Name = "Angular Blue Boots",
                Description =
                    "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                Price = 18000,
                PictureURL = "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png",
                Brand = "Angular",
                Type = "Boots",
                QuantityInStock = 100
            },
        };

        foreach (var product in products)
        {
            dbContext.Products.Add(product);
        }

        await dbContext.SaveChangesAsync();
    }
}