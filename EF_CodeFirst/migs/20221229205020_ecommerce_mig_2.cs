using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCodeFirst.migs
{
    /// <inheritdoc />
    public partial class ecommercemig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Quanitty",
                table: "Products",
                newName: "Quanity");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Quanity",
                table: "Products",
                newName: "Quanitty");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
