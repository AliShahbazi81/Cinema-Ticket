using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3801), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3803), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3805), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3807), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3808), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3810), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3812), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3813), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3815), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3816), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3818), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 22, 5, 36, 3, 231, DateTimeKind.Utc).AddTicks(3819), "https://www.pngmart.com/files/1/Nike-Shoes-Transparent-PNG.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6022), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6025), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6027), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6029), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6031), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6032), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6034), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6036), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6037), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6039), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6041), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreationTime", "PictureURL" },
                values: new object[] { new DateTime(2022, 12, 19, 5, 21, 13, 569, DateTimeKind.Utc).AddTicks(6042), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.adidas.ca%2Fen%2Fwomen-superstar&psig=AOvVaw12H4_Aokgc686UoNzG7nMk&ust=1669258458275000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMj0172mw_sCFQAAAAAdAAAAABAE" });
        }
    }
}
