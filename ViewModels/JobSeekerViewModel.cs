using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Helpers;
using SoftwareCompanyApp.Models;
using SoftwareCompanyApp.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SoftwareCompanyApp.ViewModels
{
    public class JobSeekerViewModel : INotifyPropertyChanged
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
        private bool _isActive;

        private readonly JobSeekerService _jobSeekerService;
        private ObservableCollection<Skill> _availableSkills;
        private ObservableCollection<Skill> _selectedSkills;
        private Skill _selectedSkill;

        public ObservableCollection<Skill> AvailableSkills
        {
            get => _availableSkills;
            set { _availableSkills = value; OnPropertyChanged(); }
        }

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

        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; OnPropertyChanged(); }
        }

        public ICommand SaveJobSeekerCommand { get; set; }
        public ICommand AddSkillCommand { get; set; }
        public ApplicationDbContext _context;
        public int _jobSeekerId { get; set; }

        public JobSeekerViewModel()
        {
            _context = App.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _jobSeekerService = App.ServiceProvider.GetRequiredService<JobSeekerService>();
            _ = LoadData();
            SaveJobSeekerCommand = new RelayCommand(param => SaveJobSeeker());
            AddSkillCommand = new RelayCommand(param => AddSkill());
        }


        private async Task LoadData()
        {
            try
            {
                // Ожидаем завершения загрузки навыков
                var skills = await _context.Skills.ToListAsync();
                AvailableSkills = new ObservableCollection<Skill>(skills);
                SelectedSkills = new ObservableCollection<Skill>(); // Инициализация выбранных навыков
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading skills: {ex.Message}");
            }
        }

        private void AddSkill()
        {
            if (SelectedSkill != null && !SelectedSkills.Contains(SelectedSkill))
            {
                SelectedSkills.Add(SelectedSkill);
                SelectedSkill = null;
            }
        }

        public void LoadJobSeekerData(JobSeeker jobSeeker)
        {
            if (jobSeeker != null)
            {
                _jobSeekerId = jobSeeker.Id;
                FirstName = jobSeeker.FirstName;
                LastName = jobSeeker.LastName;
                Phone = jobSeeker.Phone;
                Email = jobSeeker.Email;
                Description = jobSeeker.Description;
                Position = jobSeeker.Position;
                Location = jobSeeker.Location;
                SalaryFrom = jobSeeker.SalaryFrom;
                SalaryTo = jobSeeker.SalaryTo;
            }
        }


        private void SaveJobSeeker()
        {
            try
            {
                JobSeeker jobSeeker;

                if (_jobSeekerId != 0) // Обновление существующей вакансии
                {
                    jobSeeker = _jobSeekerService.GetJobSeekerById(_jobSeekerId);

                    if (jobSeeker == null)
                    {
                        MessageBox.Show("Vacancy not found for update.");
                        return;
                    }

                    // Обновляем данные вакансии
                    jobSeeker.FirstName = FirstName;
                    jobSeeker.LastName = LastName;
                    jobSeeker.Email = Email;
                    jobSeeker.Phone = Phone;
                    jobSeeker.Location = Location;
                    jobSeeker.Position = Position;
                    jobSeeker.Description = Description;
                    jobSeeker.SalaryFrom = SalaryFrom;
                    jobSeeker.SalaryTo = SalaryTo;
                    jobSeeker.IsActive = IsActive;

                    // Обновляем связанные скиллы: удаляем старые и добавляем новые
                    _jobSeekerService.UpdateJobSeekerSkills(jobSeeker.Id, SelectedSkills);
                    _jobSeekerService.UpdateJobSeeker(jobSeeker);

                    MessageBox.Show("Vacancy updated successfully!");
                }
                else // Создание новой вакансии
                {
                    jobSeeker = new JobSeeker
                    {
                        FirstName = _firstName,
                        LastName = _lastName,
                        Email = _email,
                        Phone = _phone,
                        Location = _location,
                        Position = _position,
                        Description = _description,
                        SalaryFrom = _salaryFrom,
                        SalaryTo = _salaryTo,
                        IsActive = _isActive
                    };

                    _jobSeekerService.AddJobSeeker(jobSeeker);
                    _jobSeekerService.AddJobSeekerSkills(jobSeeker.Id, SelectedSkills);

                    MessageBox.Show("JobSeeker saved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving vacancy: {ex.Message}");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
