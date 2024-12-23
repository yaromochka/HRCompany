using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Models;
using SoftwareCompanyApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SoftwareCompanyApp.ViewModels
{
    public class OneVacancyViewModel : INotifyPropertyChanged
    {
        private string _title;
        private string _company;
        private string _description;
        private string _requirments;
        private int _salaryFrom;
        private int _salaryTo;
        private int _employmentTypeId;
        private ObservableCollection<VacancySkill> _vacancySkills;
        private ObservableCollection<Skill> _skillsList;

        private readonly VacancyService _vacancyService;

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public string Company
        {
            get => _company;
            set { _company = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public string Requirments
        {
            get => _requirments;
            set { _requirments = value; OnPropertyChanged(); }
        }

        public int SalaryFrom
        {
            get => _salaryFrom;
            set { _salaryFrom = value; OnPropertyChanged(); }
        }

        public int SalaryTo
        {
            get => _salaryTo;
            set { _salaryTo = value; OnPropertyChanged(); }
        }

        public int EmploymentTypeId
        {
            get => _employmentTypeId;
            set { _employmentTypeId = value; OnPropertyChanged(); }
        }

        public ObservableCollection<VacancySkill> VacancySkills
        {
            get => _vacancySkills;
            set
            {
                if (!ReferenceEquals(_vacancySkills, value))
                {
                    _vacancySkills = value;
                    UpdateSkills(); // Обновляем SkillsList при изменении VacancySkills
                }
            }
        }
        public ObservableCollection<Skill> SkillsList
        {
            get => _skillsList;
            set
            {
                if (!ReferenceEquals(_skillsList, value))
                {
                    _skillsList = value;
                    Debug.WriteLine($"SkillsList updated. Count: {_skillsList?.Count ?? 0}");
                    OnPropertyChanged(nameof(SkillsList));
                }
            }
        }

        private ApplicationDbContext _dbContext;

        public OneVacancyViewModel()
        {
            _dbContext = App.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _vacancyService = App.ServiceProvider.GetRequiredService<VacancyService>();
            VacancySkills = new ObservableCollection<VacancySkill>();
        }

        public void LoadVacancyData(Vacancy vacancy)
        {
            if (vacancy != null)
            {
                Title = vacancy.Title;
                Company = vacancy.Company;
                Description = vacancy.Description;
                Requirments = vacancy.Requirments;
                SalaryFrom = vacancy.SalaryFrom;
                SalaryTo = vacancy.SalaryTo;
                EmploymentTypeId = vacancy.EmploymentTypeId;

                // Загружаем связанные навыки
                var vacancySkills = _dbContext.VacancySkills
                    .Where(vs => vs.VacancyId == vacancy.Id)
                    .Include(vs => vs.Skill) // если необходимо
                    .ToList();

                // Преобразуем их в коллекцию Skills
                SkillsList = new ObservableCollection<Skill>(
                    vacancySkills.Where(vs => vs.Skill != null).Select(vs => vs.Skill)
                );

                Debug.WriteLine($"SkillsList updated. Count: {SkillsList.Count}");
            }
        }


        private void UpdateSkills()
        {
            if (VacancySkills != null)
            {
                SkillsList = new ObservableCollection<Skill>(
                    VacancySkills.Where(vs => vs.Skill != null).Select(vs => vs.Skill)
                );
            }
            else
            {
                SkillsList = new ObservableCollection<Skill>();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
