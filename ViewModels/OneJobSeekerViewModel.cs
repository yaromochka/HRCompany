using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Models;
using SoftwareCompanyApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareCompanyApp.ViewModels
{
    internal class OneJobSeekerViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _description;
        private string _location;
        private string _position;
        private int _salaryFrom;
        private int _salaryTo;

        private readonly JobSeekerService _jobSeekerService;
        private ObservableCollection<Skill> _selectedSkills;
        private Skill _selectedSkill;


        public ObservableCollection<Skill> SelectedSkills
        {
            get => _selectedSkills;
            set { _selectedSkills = value; OnPropertyChanged(); }
        }

        public Skill SelectedSkill
        {
            get => _selectedSkill;
            set { _selectedSkill = value; OnPropertyChanged(); }
        }

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(); }
        }

        public string Location
        {
            get => _location;
            set { _location = value; OnPropertyChanged(); }
        }

        public string Position
        {
            get => _position;
            set { _position = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
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

        public string FullName => $"{FirstName} {LastName}";

        public OneJobSeekerViewModel()
        {
            _jobSeekerService = App.ServiceProvider.GetRequiredService<JobSeekerService>();
            SelectedSkills = new ObservableCollection<Skill>();
        }

        public void LoadJobSeekerData(JobSeeker jobSeeker)
        {
            if (jobSeeker != null)
            {
                FirstName = jobSeeker.FirstName;
                LastName = jobSeeker.LastName;
                Phone = jobSeeker.Phone;
                Email = jobSeeker.Email;
                Description = jobSeeker.Description;
                Position = jobSeeker.Position;
                Location = jobSeeker.Location;
                SalaryFrom = jobSeeker.SalaryFrom;
                SalaryTo = jobSeeker.SalaryTo;

                // Загружаем связанные навыки
                LoadJobSeekerSkills(jobSeeker.Id);
            }
        }

        private void LoadJobSeekerSkills(int jobSeekerId)
        {
            // Получаем dbContext из контейнера DI (не уничтожаем его вручную)
            var dbContext = App.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Загружаем связанные навыки через JobSeekerSkills
            var jobSeekerSkills = dbContext.JobSeekerSkills
                .Where(js => js.JobSeekerId == jobSeekerId)
                .Include(js => js.Skill) // Убедитесь, что связанные Skill загружаются
                .ToList();

            // Преобразуем их в коллекцию SelectedSkills
            SelectedSkills = new ObservableCollection<Skill>(
                jobSeekerSkills.Where(js => js.Skill != null).Select(js => js.Skill)
            );
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
