using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareCompanyApp.Migrations
{
    /// <inheritdoc />
    public partial class AddVacancyToJobSeekerSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekerSkills_Vacancies_VacancyId",
                table: "JobSeekerSkills");

            migrationBuilder.AlterColumn<int>(
                name: "VacancyId",
                table: "JobSeekerSkills",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekerSkills_Vacancies_VacancyId",
                table: "JobSeekerSkills",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekerSkills_Vacancies_VacancyId",
                table: "JobSeekerSkills");

            migrationBuilder.AlterColumn<int>(
                name: "VacancyId",
                table: "JobSeekerSkills",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekerSkills_Vacancies_VacancyId",
                table: "JobSeekerSkills",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id");
        }
    }
}
