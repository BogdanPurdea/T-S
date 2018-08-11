using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DAL.EF.Migrations
{
    public partial class BillingAndShippingDetailsForOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                schema: "Store",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                schema: "Store",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                schema: "Store",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress",
                schema: "Store",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                schema: "Store",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                schema: "Store",
                table: "Orders");
        }
    }
}
