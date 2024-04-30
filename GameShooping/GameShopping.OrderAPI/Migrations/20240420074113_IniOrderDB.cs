using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShopping.OrderAPI.Migrations
{
    public partial class IniOrderDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order_header",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_id = table.Column<string>(type: "TEXT", nullable: true),
                    coupon_code = table.Column<string>(type: "TEXT", nullable: true),
                    purchase_amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    discount_amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: true),
                    last_name = table.Column<string>(type: "TEXT", nullable: true),
                    purchase_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    order_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    phone_number = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    card_number = table.Column<string>(type: "TEXT", nullable: true),
                    cvv = table.Column<string>(type: "TEXT", nullable: true),
                    expiry_month_year = table.Column<string>(type: "TEXT", nullable: true),
                    total_itens = table.Column<int>(type: "INTEGER", nullable: false),
                    payment_status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_header", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderHeaderId = table.Column<long>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<long>(type: "INTEGER", nullable: false),
                    count = table.Column<int>(type: "INTEGER", nullable: false),
                    product_name = table.Column<string>(type: "TEXT", nullable: true),
                    price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_detail_order_header_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "order_header",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_OrderHeaderId",
                table: "order_detail",
                column: "OrderHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropTable(
                name: "order_header");
        }
    }
}
