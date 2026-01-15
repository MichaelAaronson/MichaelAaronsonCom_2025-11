using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class RenameLastNmeToLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastNme",
                table: "Person",
                newName: "LastName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Person",
                newName: "LastNme");
        }
    }
}
