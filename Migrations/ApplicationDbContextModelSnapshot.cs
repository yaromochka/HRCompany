﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;

#nullable disable

namespace SoftwareCompanyApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "C#"
                        },
                        new
                        {
                            Id = 2,
                            Name = "JavaScript"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Python"
                        },
                        new
                        {
                            Id = 4,
                            Name = "SQL"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Git"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Agile"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Scrum"
                        });
                });

            modelBuilder.Entity("SoftwareCompanyApp.Models.EmploymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmploymentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Полная занятость"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Частичная занятость"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Проектная работа"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Стажировка"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Удаленная работа"
                        });
                });

            modelBuilder.Entity("SoftwareCompanyApp.Models.JobSeeker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SalaryFrom")
                        .HasColumnType("integer");

                    b.Property<int>("SalaryTo")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("JobSeekers");
                });

            modelBuilder.Entity("SoftwareCompanyApp.Models.JobSeekerSkill", b =>
                {
                    b.Property<int>("JobSeekerId")
                        .HasColumnType("integer")
                        .HasColumnOrder(0);

                    b.Property<int>("SkillId")
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    b.HasKey("JobSeekerId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("JobSeekerSkills");
                });

            modelBuilder.Entity("SoftwareCompanyApp.Models.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EmploymentTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Requirments")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SalaryFrom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SalaryTo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("SoftwareCompanyApp.Models.VacancySkill", b =>
                {
                    b.Property<int>("VacancyId")
                        .HasColumnType("integer");

                    b.Property<int>("SkillId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("VacancyId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("VacancySkills");
                });

            modelBuilder.Entity("SoftwareCompanyApp.Models.JobSeekerSkill", b =>
                {
                    b.HasOne("SoftwareCompanyApp.Models.JobSeeker", "JobSeeker")
                        .WithMany("JobSeekerSkills")
                        .HasForeignKey("JobSeekerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skill", "Skill")
                        .WithMany("JobSeekerSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeeker");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("SoftwareCompanyApp.Models.VacancySkill", b =>
                {
                    b.HasOne("Skill", "Skill")
                        .WithMany("VacancySkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareCompanyApp.Models.Vacancy", "Vacancy")
                        .WithMany("VacancySkills")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("Skill", b =>
                {
                    b.Navigation("JobSeekerSkills");

                    b.Navigation("VacancySkills");
                });

            modelBuilder.Entity("SoftwareCompanyApp.Models.JobSeeker", b =>
                {
                    b.Navigation("JobSeekerSkills");
                });

            modelBuilder.Entity("SoftwareCompanyApp.Models.Vacancy", b =>
                {
                    b.Navigation("VacancySkills");
                });
#pragma warning restore 612, 618
        }
    }
}
