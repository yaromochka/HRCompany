using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareCompanyApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEmploymentTypeToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_EmploymentTypes_EmploymentTypeId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_EmploymentTypeId",
                table: "Vacancies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_EmploymentTypeId",
                table: "Vacancies",
                column: "EmploymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_EmploymentTypes_EmploymentTypeId",
                table: "Vacancies",
                column: "EmploymentTypeId",
                principalTable: "EmploymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
