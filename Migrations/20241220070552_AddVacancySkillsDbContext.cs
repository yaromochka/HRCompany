using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SoftwareCompanyApp.Migrations
{
    /// <inheritdoc />
    public partial class AddVacancySkillsDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekerSkills_Vacancies_VacancyId",
                table: "JobSeekerSkills");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekerSkills_VacancyId",
                table: "JobSeekerSkills");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "JobSeekerSkills");

            migrationBuilder.CreateTable(
                name: "VacancySkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VacancyId = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancySkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacancySkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacancySkills_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacancySkills_SkillId",
                table: "VacancySkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_VacancySkills_VacancyId",
                table: "VacancySkills",
                column: "VacancyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacancySkills");

            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "JobSeekerSkills",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_VacancyId",
                table: "JobSeekerSkills",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekerSkills_Vacancies_VacancyId",
                table: "JobSeekerSkills",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
