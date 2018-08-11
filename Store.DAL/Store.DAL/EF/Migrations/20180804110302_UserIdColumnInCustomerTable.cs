using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DAL.EF.Migrations
{
    public partial class UserIdColumnInCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Store",
                table: "Customers",
                nullable: true,
                maxLength: 256);
            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                schema: "Store",
                table: "Customers",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                schema: "Store",
                table: "Customers");
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Store",
                table: "Customers");
        }
    }
}
