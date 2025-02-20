﻿using SoftwareCompanyApp.Data;
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
using System.Diagnostics;

public class VacancyViewModel : INotifyPropertyChanged
{
    private string _title;
    private string _company;
    private string _description;
    private string _requirements;
    private int _salaryFrom;
    private int _salaryTo;
    private EmploymentType _employmentType;
    private int? _employmentTypeId;
    private ObservableCollection<EmploymentType> _employmentTypes;  // Используем ObservableCollection

    private readonly ApplicationDbContext _context;
    private readonly ApplicationDbContext _skillContext;
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

    public int SalaryFrom
    {
        get => _salaryFrom;
        set
        {
            _salaryFrom = value;
            OnPropertyChanged();
        }
    }

    public int SalaryTo
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
    public ICommand RemoveSkillCommand { get; set; }
    public int _vacancyId { get; set; }

    public VacancyViewModel()
    {
        _context = App.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _skillContext = App.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _vacancyService = App.ServiceProvider.GetRequiredService<VacancyService>();
        // Запуск методов асинхронно, поочередно
        _ = LoadDataAsync();
        // После загрузки данных можно инициализировать команды
        SaveVacancyCommand = new RelayCommand(param => SaveVacancy());
        AddSkillCommand = new RelayCommand(param => AddSkill());
        RemoveSkillCommand = new RelayCommand(param => RemoveSkill(param as Skill));
    }

    private async Task LoadDataAsync()
    {
        // Загружаем типы занятости сначала
        await LoadEmploymentTypes();

        // Затем загружаем навыки
        await LoadSkills();
    }

    private async Task LoadEmploymentTypes()
    {
        try
        {
            // Ожидаем завершения загрузки типов занятости
            var types = await _context.EmploymentTypes.ToListAsync();
            if (types == null || types.Count == 0)
            {
                MessageBox.Show("No employment types available.");
                return;
            }
            EmploymentTypes = new ObservableCollection<EmploymentType>(types);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading employment types: {ex.Message}");
        }
    }

    private async Task LoadSkills()
    {
        try
        {
            // Ожидаем завершения загрузки навыков
            var skills = await _context.Skills.ToListAsync();
            AvailableSkills = new ObservableCollection<Skill>(skills);
            if (SelectedSkills == null || !SelectedSkills.Any())
            {
                // Если пустая или еще не была инициализирована, инициализируем коллекцию
                SelectedSkills = new ObservableCollection<Skill>();
            }
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

        try
        {
            Vacancy vacancy;

            if (_vacancyId != 0) // Обновление существующей вакансии
            {
                vacancy = _vacancyService.GetVacancyById(_vacancyId);

                if (vacancy == null)
                {
                    MessageBox.Show("Vacancy not found for update.");
                    return;
                }

                // Обновляем данные вакансии
                vacancy.Title = Title;
                vacancy.Company = Company;
                vacancy.Description = Description;
                vacancy.Requirments = Requirements;
                vacancy.SalaryFrom = SalaryFrom;
                vacancy.SalaryTo = SalaryTo;
                vacancy.EmploymentTypeId = EmploymentTypeId.Value;

                // Обновляем связанные скиллы: удаляем старые и добавляем новые
                _vacancyService.UpdateVacancySkills(vacancy.Id, SelectedSkills.ToList());
                _vacancyService.UpdateVacancy(vacancy);

                MessageBox.Show("Vacancy updated successfully!");
            }
            else // Создание новой вакансии
            {
                vacancy = new Vacancy
                {
                    Title = Title,
                    Company = Company,
                    Description = Description,
                    Requirments = Requirements,
                    SalaryFrom = SalaryFrom,
                    SalaryTo = SalaryTo,
                    EmploymentTypeId = EmploymentTypeId.Value
                };

                // Добавляем новую вакансию и сохраняем её
                _vacancyService.AddVacancy(vacancy);

                // Теперь добавляем связанные скиллы, используя сгенерированный ID
                _vacancyService.AddVacancySkills(vacancy.Id, SelectedSkills.ToList());

                MessageBox.Show("Vacancy saved successfully!");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving vacancy: {ex.Message}");
        }
    }

    public void LoadVacancyData(Vacancy vacancy)
    {
        try
        {
            if (vacancy == null)
            {
                MessageBox.Show("Vacancy not found.");
                return;
            }

            // Заполняем свойства ViewModel на основе данных вакансии
            _vacancyId = vacancy.Id;
            Title = vacancy.Title;
            Company = vacancy.Company;
            Description = vacancy.Description;
            Requirements = vacancy.Requirments;
            SalaryFrom = vacancy.SalaryFrom;
            SalaryTo = vacancy.SalaryTo;
            EmploymentTypeId = vacancy.EmploymentTypeId;

            var vacancySkills = _skillContext.VacancySkills
                .Where(vs => vs.VacancyId == vacancy.Id)
                .Include(vs => vs.Skill) // если необходимо
                .ToList();

            // Преобразуем их в коллекцию Skills
            SelectedSkills = new ObservableCollection<Skill>(
                vacancySkills.Where(vs => vs.Skill != null).Select(vs => vs.Skill)
            );

            // Загружаем тип занятости
            if (EmploymentTypes != null && EmploymentTypes.Any())
            {
                var employmentType = EmploymentTypes.FirstOrDefault(et => et.Id == EmploymentTypeId);
                EmploymentType = employmentType ?? throw new Exception($"Employment type with ID {EmploymentTypeId} not found.");
            }
            else
            {
                MessageBox.Show("Employment Types are not loaded or available.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading vacancy data: {ex.Message}");
        }
    }
    private void RemoveSkill(Skill skill)
    {
        if (skill != null && SelectedSkills.Contains(skill))
        {
            SelectedSkills.Remove(skill);
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}