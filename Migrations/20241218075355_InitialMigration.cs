using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SoftwareCompanyApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "JobSeekers",
                newName: "Position");

            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "JobSeekerSkills",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JobSeekers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "JobSeekers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "JobSeekers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "JobSeekers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "JobSeekers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobSeekers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "JobSeekers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SalaryFrom",
                table: "JobSeekers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalaryTo",
                table: "JobSeekers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Company = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Requirments = table.Column<string>(type: "text", nullable: false),
                    SalaryFrom = table.Column<string>(type: "text", nullable: false),
                    SalaryTo = table.Column<string>(type: "text", nullable: false),
                    EmploymentTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacancies_EmploymentTypes_EmploymentTypeId",
                        column: x => x.EmploymentTypeId,
                        principalTable: "EmploymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_VacancyId",
                table: "JobSeekerSkills",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_EmploymentTypeId",
                table: "Vacancies",
                column: "EmploymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekerSkills_Vacancies_VacancyId",
                table: "JobSeekerSkills",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekerSkills_Vacancies_VacancyId",
                table: "JobSeekerSkills");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekerSkills_VacancyId",
                table: "JobSeekerSkills");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "JobSeekerSkills");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "SalaryFrom",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "SalaryTo",
                table: "JobSeekers");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "JobSeekers",
                newName: "FullName");
        }
    }
}
