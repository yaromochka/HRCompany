using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SoftwareCompanyApp.Migrations
{
    /// <inheritdoc />
    public partial class VacabcyToSkillsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VacancySkills",
                table: "VacancySkills");

            migrationBuilder.DropIndex(
                name: "IX_VacancySkills_VacancyId",
                table: "VacancySkills");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "VacancySkills",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacancySkills",
                table: "VacancySkills",
                columns: new[] { "VacancyId", "SkillId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VacancySkills",
                table: "VacancySkills");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "VacancySkills",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacancySkills",
                table: "VacancySkills",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VacancySkills_VacancyId",
                table: "VacancySkills",
                column: "VacancyId");
        }
    }
}
