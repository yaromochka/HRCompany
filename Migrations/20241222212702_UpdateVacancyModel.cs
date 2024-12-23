using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareCompanyApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVacancyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Vacancies_VacancyId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_VacancyId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "Skills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "Skills",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1,
                column: "VacancyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2,
                column: "VacancyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                column: "VacancyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4,
                column: "VacancyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5,
                column: "VacancyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6,
                column: "VacancyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 7,
                column: "VacancyId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_VacancyId",
                table: "Skills",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Vacancies_VacancyId",
                table: "Skills",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id");
        }
    }
}
