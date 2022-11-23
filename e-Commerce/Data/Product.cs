using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_Commerce.Data;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long Price { get; set; }
    public string PictureURL { get; set; }
    public string Type { get; set; }
    public string Brand { get; set; }
    public int QuantityInStock { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
}

public class ProductBuilder : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired();
        builder.Property(x => x.Description)
            .IsRequired();
        builder.Property(x => x.Price)
            .IsRequired();
        
    }
}
