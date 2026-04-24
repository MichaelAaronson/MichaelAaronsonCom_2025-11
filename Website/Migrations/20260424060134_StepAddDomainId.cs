using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class StepAddDomainId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DomainId",
                table: "Step",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Step_DomainId",
                table: "Step",
                column: "DomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Domain_DomainId",
                table: "Step",
                column: "DomainId",
                principalTable: "Domain",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_Domain_DomainId",
                table: "Step");

            migrationBuilder.DropIndex(
                name: "IX_Step_DomainId",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "Step");
        }
    }
}
