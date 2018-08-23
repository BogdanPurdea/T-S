using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DAL.EF.Migrations
{
    public partial class RemovePassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                schema: "Store",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "Store",
                table: "Customers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
