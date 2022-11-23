using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class InitializingDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    PictureURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CreationTime", "Description", "IsDeleted", "Name", "PictureURL", "Price", "QuantityInStock", "Type" },
                values: new object[,]
                {
                    { 1, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7901), "This is our 1st Product", false, "1st Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 10000L, 100, "Sports" },
                    { 2, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7904), "This is our 2nd Product", false, "2nd Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 20000L, 200, "Sports2" },
                    { 4, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7905), "This is our 4th Product", false, "4th Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 40000L, 400, "Sports4" },
                    { 5, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7907), "This is our 5th Product", false, "5th Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 50000L, 500, "Sports5" },
                    { 6, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7908), "This is our 6th Product", false, "6th Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 60000L, 600, "Sports6" },
                    { 7, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7910), "This is our 7th Product", false, "7th Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 70000L, 700, "Sports7" },
                    { 8, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7911), "This is our 8th Product", false, "8th Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 80000L, 800, "Sports8" },
                    { 9, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7913), "This is our 9th Product", false, "9th Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 90000L, 900, "Sports9" },
                    { 10, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7915), "This is our 10th Product", false, "10th Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 100000L, 1000, "Sports10" },
                    { 11, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7916), "This is our 11th Product", false, "11th Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 110000L, 1100, "Sports11" },
                    { 12, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7918), "This is our 12th Product", false, "12th Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 120000L, 1200, "Sports12" },
                    { 13, "Adidas", new DateTime(2022, 11, 23, 4, 38, 5, 758, DateTimeKind.Utc).AddTicks(7919), "This is our 13th Product", false, "13th Product", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE", 130000L, 1300, "Sports13" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
