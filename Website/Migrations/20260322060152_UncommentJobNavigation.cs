using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class UncommentJobNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JobDetail_JobId",
                table: "JobDetail",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobDetail_Job_JobId",
                table: "JobDetail",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobDetail_Job_JobId",
                table: "JobDetail");

            migrationBuilder.DropIndex(
                name: "IX_JobDetail_JobId",
                table: "JobDetail");
        }
    }
}
