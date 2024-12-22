using Microsoft.Extensions.DependencyInjection;
using SoftwareCompanyApp.Models;
using SoftwareCompanyApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            set { _vacancySkills = value; OnPropertyChanged(); }
        }

        public OneVacancyViewModel()
        {
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
                VacancySkills = new ObservableCollection<VacancySkill>(vacancy.VacancySkills ?? new List<VacancySkill>());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
