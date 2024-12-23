using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareCompanyApp.Migrations
{
    /// <inheritdoc />
    public partial class AddVacancyAndJobSeekerSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "JobSeekers");

            migrationBuilder.AddColumn<int>(
                name: "JobSeekerId",
                table: "Skills",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "Skills",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "JobSeekerId", "VacancyId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "JobSeekerId", "VacancyId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "JobSeekerId", "VacancyId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "JobSeekerId", "VacancyId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "JobSeekerId", "VacancyId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "JobSeekerId", "VacancyId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "JobSeekerId", "VacancyId" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_JobSeekerId",
                table: "Skills",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_VacancyId",
                table: "Skills",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_JobSeekers_JobSeekerId",
                table: "Skills",
                column: "JobSeekerId",
                principalTable: "JobSeekers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Vacancies_VacancyId",
                table: "Skills",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_JobSeekers_JobSeekerId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Vacancies_VacancyId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_JobSeekerId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_VacancyId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "JobSeekerId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "Skills");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "JobSeekers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
