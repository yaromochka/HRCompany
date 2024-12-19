using SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Models;
using SoftwareCompanyApp.Helpers;
using System;
using System.Collections.ObjectModel;  // Обязательно добавьте эту директиву
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Windows;
using SoftwareCompanyApp.Services;
using SoftwareCompanyApp;
using Microsoft.Extensions.DependencyInjection;

public class VacancyViewModel : INotifyPropertyChanged
{
    private string _title;
    private string _company;
    private string _description;
    private string _requirements;
    private string _salaryFrom;
    private string _salaryTo;
    private EmploymentType _employmentType;
    private int? _employmentTypeId;
    private ObservableCollection<EmploymentType> _employmentTypes;  // Используем ObservableCollection

    private readonly ApplicationDbContext _context;
    private VacancyService _vacancyService;
    private ObservableCollection<Skill> _availableSkills;
    private ObservableCollection<Skill> _selectedSkills;
    private Skill _selectedSkill;  // Текущий выбранный навык
    public ICommand AddSkillCommand { get; set; }


    public ObservableCollection<Skill> AvailableSkills
    {
        get => _availableSkills;
        set
        {
            _availableSkills = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Skill> SelectedSkills
    {
        get => _selectedSkills;
        set
        {
            _selectedSkills = value;
            OnPropertyChanged();
        }
    }

    public Skill SelectedSkill
    {
        get => _selectedSkill;
        set
        {
            _selectedSkill = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<EmploymentType> EmploymentTypes
    {
        get => _employmentTypes;
        set
        {
            _employmentTypes = value;
            OnPropertyChanged();
        }
    }

    public int? EmploymentTypeId
    {
        get => _employmentTypeId;
        set
        {
            _employmentTypeId = value;
            OnPropertyChanged();
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string Company
    {
        get => _company;
        set
        {
            _company = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    public string Requirements
    {
        get => _requirements;
        set
        {
            _requirements = value;
            OnPropertyChanged();
        }
    }

    public string SalaryFrom
    {
        get => _salaryFrom;
        set
        {
            _salaryFrom = value;
            OnPropertyChanged();
        }
    }

    public string SalaryTo
    {
        get => _salaryTo;
        set
        {
            _salaryTo = value;
            OnPropertyChanged();
        }
    }

    public EmploymentType EmploymentType
    {
        get => _employmentType;
        set
        {
            _employmentType = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveVacancyCommand { get; set; }

    public VacancyViewModel(ApplicationDbContext dbContext)
    {
        _context = dbContext;
        _vacancyService = App.ServiceProvider.GetRequiredService<VacancyService>(); ;
        // Запуск методов асинхронно, поочередно
        LoadDataAsync();
        // После загрузки данных можно инициализировать команды
        SaveVacancyCommand = new RelayCommand(param => SaveVacancy());
        AddSkillCommand = new RelayCommand(param => AddSkill());
    }

    private async void LoadDataAsync()
    {
        // Сначала загружаем типы занятости
        await LoadEmploymentTypes();

        // Затем загружаем навыки
        await LoadSkills();
    }

    private async Task LoadSkills()
    {
        // Загрузка доступных навыков из базы данных
        var skills = await _context.Skills.ToListAsync();
        AvailableSkills = new ObservableCollection<Skill>(skills);
        SelectedSkills = new ObservableCollection<Skill>();  // Инициализация списка выбранных навыков
    }

    private async Task LoadEmploymentTypes()
    {
        // Получаем данные из базы данных
        var types = await _context.EmploymentTypes.ToListAsync();

        // Преобразуем в ObservableCollection для привязки
        EmploymentTypes = new ObservableCollection<EmploymentType>(types);
    }


    private void AddSkill()
    {
        if (SelectedSkill != null && !SelectedSkills.Contains(SelectedSkill))
        {
            SelectedSkills.Add(SelectedSkill);
            SelectedSkill = null;  // Сбросить текущий выбранный навык после добавления
        }
    }


    private void SaveVacancy()
    {

        if (EmploymentTypeId == null)
        {
            MessageBox.Show("Please select a valid Employment Type.");
            return;
        }

        var vacancy = new Vacancy
        {
            Title = Title,
            Company = Company,
            Description = Description,
            Requirments = Requirements,
            SalaryFrom = SalaryFrom,
            SalaryTo = SalaryTo,
            EmploymentTypeId = EmploymentTypeId.Value, // Передаем ID типа работы
            JobSeekerSkills = new List<JobSeekerSkill>() // Добавьте обработку навыков
        };

        try
        {
            _vacancyService.AddVacancy(vacancy);
            MessageBox.Show("Vacancy saved successfully!");
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
