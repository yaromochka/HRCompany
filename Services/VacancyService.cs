using Microsoft.EntityFrameworkCore;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareCompanyApp.Services
{
    public class VacancyService
    {
        public ObservableCollection<Vacancy> Vacancies { get; set;  }

        private readonly ApplicationDbContext _context;

        public VacancyService(ApplicationDbContext context)
        {
            _context = context;
            Vacancies = new ObservableCollection<Vacancy>(_context.Vacancies.ToList());
        }


        public void RemoveVacancy(Vacancy vacancy)
        {
            _context.Vacancies.Remove(vacancy);
            _context.SaveChanges();
            Vacancies.Remove(vacancy);
        }

        public void UpdateVacancy(Vacancy vacancy)
        {
            var existingVacancy = _context.Vacancies.FirstOrDefault(v => v.Id == vacancy.Id);

            if (existingVacancy != null)
            {
                existingVacancy.Title = vacancy.Title;
                existingVacancy.Company = vacancy.Company;
                existingVacancy.Description = vacancy.Description;
                existingVacancy.Requirments = vacancy.Requirments;
                existingVacancy.SalaryFrom = vacancy.SalaryFrom;
                existingVacancy.SalaryTo = vacancy.SalaryTo;
                existingVacancy.EmploymentTypeId = vacancy.EmploymentTypeId;

                _context.Vacancies.Attach(existingVacancy);
                _context.Entry(existingVacancy).State = EntityState.Modified;
                _context.SaveChanges();
                _ = ReloadVacancies();

            }
        }

        public void AddVacancy(Vacancy vacancy)
        {
            _context.Vacancies.Add(vacancy);
            _context.SaveChanges();
            Vacancies.Add(vacancy);
        }

        public Vacancy GetVacancyById(int vacancyId)
        {
            // Получаем вакансию по ID
            var vacancy = _context.Vacancies
                .FirstOrDefault(v => v.Id == vacancyId);

            if (vacancy == null)
            {
                MessageBox.Show("Vacancy not found.");
                return null;
            }

            // Загружаем EmploymentType по Id вакансии
            vacancy.EmploymentTypeId = _context.EmploymentTypes
                .Where(et => et.Id == vacancy.EmploymentTypeId)
                .Select(et => et.Id)
                .FirstOrDefault();

            // Загружаем все связанные JobSeekerSkills для вакансии
            vacancy.VacancySkills = _context.VacancySkills
                .Where(js => js.VacancyId == vacancyId)
                .ToList();

            return vacancy;
        }

        public async Task ReloadVacancies()
        {
            // Загрузка вакансий из базы данных
            var vacancies = await _context.Vacancies.ToListAsync();
            Vacancies.Clear();

            foreach (var vacancy in vacancies)
            {
                Vacancies.Add(vacancy);
            }
        }

        public void AddVacancySkills(int vacancyId, ObservableCollection<Skill> selectedSkills)
        {
            var jobSeekerSkills = selectedSkills.Select(skill => new VacancySkill
            {
                VacancyId = vacancyId,
                SkillId = skill.Id
            }).ToList();

            _context.VacancySkills.AddRange(jobSeekerSkills);
            _context.SaveChanges();
        }


        public void UpdateVacancySkills(int vacancyId, ObservableCollection<Skill> selectedSkills)
        {
            // Удаляем старые скиллы
            var existingSkills = _context.VacancySkills.Where(js => js.VacancyId == vacancyId).ToList();
            _context.VacancySkills.RemoveRange(existingSkills);

            // Добавляем новые скиллы
            var newSkills = selectedSkills.Select(skill => new VacancySkill
            {
                VacancyId = vacancyId,
                SkillId = skill.Id
            }).ToList();

            _context.VacancySkills.AddRange(newSkills);
            _context.SaveChanges();
        }
    }
}
