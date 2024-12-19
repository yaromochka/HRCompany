using Microsoft.EntityFrameworkCore;
using SoftwareCompanyApp.Models;


namespace SoftwareCompanyApp.Data
{
    using Microsoft.EntityFrameworkCore;

    namespace SoftwareCompanyApp.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

            public DbSet<JobSeeker> JobSeekers { get; set; }
            public DbSet<Vacancy> Vacancies { get; set; }
            public DbSet<EmploymentType> EmploymentTypes { get; set; }
            public DbSet<Skill> Skills { get; set; }
            public DbSet<JobSeekerSkill> JobSeekerSkills { get; set; }

            // Метод для конфигурации модели и данных по умолчанию
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Составной ключ для JobSeekerSkill
                modelBuilder.Entity<JobSeekerSkill>()
                    .HasKey(js => new { js.JobSeekerId, js.SkillId });

                // Определение отношений
                modelBuilder.Entity<JobSeekerSkill>()
                    .HasOne(js => js.JobSeeker)
                    .WithMany(j => j.JobSeekerSkills)
                    .HasForeignKey(js => js.JobSeekerId);

                modelBuilder.Entity<JobSeekerSkill>()
                    .HasOne(js => js.Skill)
                    .WithMany(s => s.JobSeekerSkills)
                    .HasForeignKey(js => js.SkillId);

                // Данные по умолчанию для EmploymentType
                modelBuilder.Entity<EmploymentType>().HasData(
                    new EmploymentType { Id = 1, Name = "Полная занятость" },
                    new EmploymentType { Id = 2, Name = "Частичная занятость" },
                    new EmploymentType { Id = 3, Name = "Проектная работа" },
                    new EmploymentType { Id = 4, Name = "Стажировка" },
                    new EmploymentType { Id = 5, Name = "Удаленная работа" }
                );

                // Данные по умолчанию для Skills
                modelBuilder.Entity<Skill>().HasData(
                    new Skill { Id = 1, Name = "C#" },
                    new Skill { Id = 2, Name = "JavaScript" },
                    new Skill { Id = 3, Name = "Python" },
                    new Skill { Id = 4, Name = "SQL" },
                    new Skill { Id = 5, Name = "Git" },
                    new Skill { Id = 6, Name = "Agile" },
                    new Skill { Id = 7, Name = "Scrum" }
                );
            }

        }
    }

}
