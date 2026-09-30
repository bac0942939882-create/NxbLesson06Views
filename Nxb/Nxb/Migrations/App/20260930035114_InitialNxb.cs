using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nxb.Migrations.App
{
    /// <inheritdoc />
    public partial class InitialNxb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Image = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                    table.CheckConstraint("CK_Banner_Status", "[Status] IN (0,1)");
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.CheckConstraint("CK_Category_Status", "[Status] IN (0,1)");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Image = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    SalePrice = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Descriptions = table.Column<string>(type: "ntext", maxLength: 1000, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.CheckConstraint("CK_Product_Price", "[Price] >= 0 AND [SalePrice] >= 0 AND [SalePrice] <= [Price]");
                    table.CheckConstraint("CK_Product_Status", "[Status] IN (0,1)");
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Banner",
                columns: new[] { "Id", "CreatedDate", "Description", "Image", "Name", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Khám phá sản phẩm mới tại Nxb", "demo-1.png", "Bộ sưu tập mới", (byte)1 },
                    { 2, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Các mẫu túi và balo dành cho bạn", "demo-2.png", "Ưu đãi hôm nay", (byte)1 },
                    { 3, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Bật trạng thái để hiển thị trên trang chủ", "demo-1.png", "Banner đang ẩn", (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedDate", "Name", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Túi xách", (byte)1 },
                    { 2, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Balo", (byte)1 },
                    { 3, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Phụ kiện", (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Descriptions", "Image", "Name", "Price", "SalePrice", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.", "demo-1.png", "Túi xách đen", 500000f, 450000f, (byte)1 },
                    { 2, 1, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.", "demo-2.png", "Túi xách đỏ", 550000f, 500000f, (byte)1 },
                    { 3, 1, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.", "demo-3.png", "Túi xách xanh", 600000f, 550000f, (byte)1 },
                    { 4, 1, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.", "demo-4.png", "Túi xách nâu", 650000f, 600000f, (byte)1 },
                    { 5, 2, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.", "demo-5.png", "Balo đen", 700000f, 650000f, (byte)1 },
                    { 6, 2, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.", "demo-6.png", "Balo xanh", 750000f, 700000f, (byte)1 },
                    { 7, 3, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.", "demo-7.png", "Ví nhỏ", 800000f, 750000f, (byte)1 },
                    { 8, 3, new DateTime(2026, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.", "demo-8.png", "Túi mini", 850000f, 800000f, (byte)1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
