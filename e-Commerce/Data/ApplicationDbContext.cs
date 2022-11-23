using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace e_Commerce.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions options) : base(options)
	{

	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasData(
                new Product
                {
                    Id = 1,
                    Name = "1st Product",
                    Description = "This is our 1st Product",
                    Price = 10000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports",
                    QuantityInStock = 100,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    Name = "2nd Product",
                    Description = "This is our 2nd Product",
                    Price = 20000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports2",
                    QuantityInStock = 200,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 4,
                    Name = "4th Product",
                    Description = "This is our 4th Product",
                    Price = 40000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports4",
                    QuantityInStock = 400,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 5,
                    Name = "5th Product",
                    Description = "This is our 5th Product",
                    Price = 50000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports5",
                    QuantityInStock = 500,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 6,
                    Name = "6th Product",
                    Description = "This is our 6th Product",
                    Price = 60000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports6",
                    QuantityInStock = 600,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 7,
                    Name = "7th Product",
                    Description = "This is our 7th Product",
                    Price = 70000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports7",
                    QuantityInStock = 700,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 8,
                    Name = "8th Product",
                    Description = "This is our 8th Product",
                    Price = 80000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports8",
                    QuantityInStock = 800,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 9,
                    Name = "9th Product",
                    Description = "This is our 9th Product",
                    Price = 90000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports9",
                    QuantityInStock = 900,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 10,
                    Name = "10th Product",
                    Description = "This is our 10th Product",
                    Price = 100000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports10",
                    QuantityInStock = 1000,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 11,
                    Name = "11th Product",
                    Description = "This is our 11th Product",
                    Price = 110000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports11",
                    QuantityInStock = 1100,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 12,
                    Name = "12th Product",
                    Description = "This is our 12th Product",
                    Price = 120000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports12",
                    QuantityInStock = 1200,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                },
                new Product
                {
                    Id = 13,
                    Name = "13th Product",
                    Description = "This is our 13th Product",
                    Price = 130000,
                    PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE",
                    Brand = "Adidas",
                    Type = "Sports13",
                    QuantityInStock = 1300,
                    IsDeleted = false,
                    CreationTime = DateTime.UtcNow
                });
    }

	public DbSet<Product> Products { get; set; }
}