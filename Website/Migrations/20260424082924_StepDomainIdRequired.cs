using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class StepDomainIdRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_Domain_DomainId",
                table: "Step");

            migrationBuilder.AlterColumn<int>(
                name: "DomainId",
                table: "Step",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Domain_DomainId",
                table: "Step",
                column: "DomainId",
                principalTable: "Domain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_Domain_DomainId",
                table: "Step");

            migrationBuilder.AlterColumn<int>(
                name: "DomainId",
                table: "Step",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Domain_DomainId",
                table: "Step",
                column: "DomainId",
                principalTable: "Domain",
                principalColumn: "Id");
        }
    }
}
