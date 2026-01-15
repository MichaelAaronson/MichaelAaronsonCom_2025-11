using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleAndDetailsToPersonGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "PersonGroup",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "PersonGroup",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "PersonGroup");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "PersonGroup");
        }
    }
}
