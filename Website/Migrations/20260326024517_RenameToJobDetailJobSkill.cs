using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class RenameToJobDetailJobSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobDetailSkill");

            migrationBuilder.CreateTable(
                name: "JobDetailJobSkill",
                columns: table => new
                {
                    JobDetailId = table.Column<int>(type: "int", nullable: false),
                    JobSkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDetailJobSkill", x => new { x.JobDetailId, x.JobSkillId });
                    table.ForeignKey(
                        name: "FK_JobDetailJobSkill_JobDetail_JobDetailId",
                        column: x => x.JobDetailId,
                        principalTable: "JobDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobDetailJobSkill_JobSkill_JobSkillId",
                        column: x => x.JobSkillId,
                        principalTable: "JobSkill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobDetailJobSkill_JobSkillId",
                table: "JobDetailJobSkill",
                column: "JobSkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobDetailJobSkill");

            migrationBuilder.CreateTable(
                name: "JobDetailSkill",
                columns: table => new
                {
                    JobDetailId = table.Column<int>(type: "int", nullable: false),
                    JobSkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDetailSkill", x => new { x.JobDetailId, x.JobSkillId });
                    table.ForeignKey(
                        name: "FK_JobDetailSkill_JobDetail_JobDetailId",
                        column: x => x.JobDetailId,
                        principalTable: "JobDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobDetailSkill_JobSkill_JobSkillId",
                        column: x => x.JobSkillId,
                        principalTable: "JobSkill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobDetailSkill_JobSkillId",
                table: "JobDetailSkill",
                column: "JobSkillId");
        }
    }
}
