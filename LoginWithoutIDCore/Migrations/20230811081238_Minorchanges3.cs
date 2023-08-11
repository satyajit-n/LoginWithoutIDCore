using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginWithoutIDCore.Migrations
{
    /// <inheritdoc />
    public partial class Minorchanges3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TokenStatus",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenStatus",
                table: "Users");
        }
    }
}
