using System.ComponentModel;
using System.Runtime.CompilerServices;
using SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Models;
using Microsoft.EntityFrameworkCore;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using System;

namespace SoftwareCompanyApp.ViewModels
{
    public class JobSeekerViewModel : INotifyPropertyChanged
    {
        // Поля для JobSeeker
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _description;
        private int _salaryFrom;
        private int _salaryTo;
        private bool _isActive;

        private readonly ApplicationDbContext _context;

        public JobSeekerViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Свойства для JobSeeker
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

        // Событие для оповещения об изменениях свойств
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для вызова события PropertyChanged
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Пример метода сохранения данных
        public void SaveJobSeeker()
        {
            var jobSeeker = new JobSeeker
            {
                FirstName = _firstName,
                LastName = _lastName,
                Email = _email,
                Phone = _phone,
                Description = _description,
                SalaryFrom = _salaryFrom,
                SalaryTo = _salaryTo,
                IsActive = _isActive
            };

            _context.JobSeekers.Add(jobSeeker);
            _context.SaveChanges();
        }

        // Метод для обновления существующего JobSeeker
        public void UpdateJobSeeker(int id)
        {
            var jobSeeker = _context.JobSeekers.Find(id);
            if (jobSeeker != null)
            {
                jobSeeker.FirstName = _firstName;
                jobSeeker.LastName = _lastName;
                jobSeeker.Email = _email;
                jobSeeker.Phone = _phone;
                jobSeeker.Description = _description;
                jobSeeker.SalaryFrom = _salaryFrom;
                jobSeeker.SalaryTo = _salaryTo;
                jobSeeker.IsActive = _isActive;

                _context.SaveChanges();
            }
        }
    }
}
