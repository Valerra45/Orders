using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Created", "Description", "Name", "Price", "Quantity", "Update" },
                values: new object[,]
                {
                    { new Guid("1ab8cc12-79ac-404d-aae8-d6a2886faf12"), new DateTime(2025, 4, 28, 10, 14, 27, 942, DateTimeKind.Unspecified).AddTicks(5506), "Description test product 2", "Product 2", 30m, 200, new DateTime(2025, 4, 28, 10, 14, 27, 942, DateTimeKind.Unspecified).AddTicks(5506) },
                    { new Guid("5c1525d6-bc65-4a5b-98d3-4a08baad5ed2"), new DateTime(2025, 4, 28, 10, 14, 27, 942, DateTimeKind.Unspecified).AddTicks(5558), "Description test product 3", "Product 3", 40m, 300, new DateTime(2025, 4, 28, 10, 14, 27, 942, DateTimeKind.Unspecified).AddTicks(5558) },
                    { new Guid("e0dcfa80-9153-4688-911f-962b8481c4f1"), new DateTime(2025, 4, 28, 10, 14, 27, 938, DateTimeKind.Unspecified).AddTicks(6963), "Description test product 1", "Product 1", 20m, 100, new DateTime(2025, 4, 28, 10, 14, 27, 938, DateTimeKind.Unspecified).AddTicks(6963) }
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
