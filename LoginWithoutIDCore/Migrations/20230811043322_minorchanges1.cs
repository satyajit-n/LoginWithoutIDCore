using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginWithoutIDCore.Migrations
{
    /// <inheritdoc />
    public partial class minorchanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Users",
                newName: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Roles",
                table: "Users",
                newName: "City");
        }
    }
}
