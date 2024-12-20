using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftwareCompany.Helpers;
using SoftwareCompanyApp;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Helpers;
using SoftwareCompanyApp.Models;
using SoftwareCompanyApp.Services;
using SoftwareCompanyApp.ViewModels;
using SoftwareCompanyApp.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

public class MainViewModel
{
    private readonly ApplicationDbContext _context;
    public ICommand NavigateToStatisticsCommand { get; }
    public ICommand NavigateToVacancyCommand { get; }
    public ICommand NavigateToJobSeekerCommand { get; }

    private readonly NavigationService _navigationService;
    private VacancyService _vacancyService;
    private JobSeekerService _jobSeekerService;

    public ObservableCollection<Vacancy> Vacancies { get; set; }

    private Vacancy _selectedVacancy;
    public Vacancy SelectedVacancy
    {
        get => _selectedVacancy;
        set
        {
            _selectedVacancy = value;
            OnPropertyChanged(nameof(SelectedVacancy));
        }
    }

    public ObservableCollection<JobSeeker> JobSeekers { get; set; }

    private JobSeeker _selectedJobSeeker;
    public JobSeeker SelectedJobSeeker
    {
        get => _selectedJobSeeker;
        set
        {
            _selectedJobSeeker = value;
            OnPropertyChanged(nameof(SelectedJobSeeker));
        }
    }
    public ICommand EditVacancyCommand { get; set; }
    public ICommand DeleteVacancyCommand { get; set; }
    public ICommand EditJobSeekerCommand { get; set; }
    public ICommand DeleteJobSeekerCommand { get; set; }

    public MainViewModel(NavigationService navigationService, ApplicationDbContext dbContext)
    {
        _navigationService = navigationService;
        _context = dbContext;

        // Команды для перехода на страницы
        NavigateToStatisticsCommand = new RelayCommand(
            param => _navigationService.Navigate(typeof(StatisticsWindow)),
            param => true);

        NavigateToVacancyCommand = new RelayCommand(
            param => _navigationService.Navigate(typeof(VacancyWindow)), // передаем VacancyViewModel
            param => true);

        NavigateToJobSeekerCommand = new RelayCommand(
            param => _navigationService.Navigate(typeof(JobSeekerWindow)),
        param => true);

        _vacancyService = App.ServiceProvider.GetRequiredService<VacancyService>();
        _jobSeekerService = App.ServiceProvider.GetRequiredService<JobSeekerService>();
        Vacancies = _vacancyService.Vacancies;
        JobSeekers = _jobSeekerService.JobSeekers;

        // Инициализация команд
        EditVacancyCommand = new RelayCommand(param => EditVacancy(param as Vacancy));
        DeleteVacancyCommand = new RelayCommand(param => DeleteVacancy(param as Vacancy));

        EditJobSeekerCommand = new RelayCommand(param => EditJobSeeker(param as JobSeeker));
        DeleteJobSeekerCommand = new RelayCommand(param => DeleteJobSeeker(param as JobSeeker));

        // Загрузить вакансии
        LoadVacancies();
        LoadJobSeekers();
    }


    private async void LoadVacancies()
    {
        var context = App.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // Загрузка вакансий из базы данных
        var vacancies = await Task.Run(() => context.Vacancies.ToList());
        Vacancies.Clear();

        foreach (var vacancy in vacancies)
        {
            Vacancies.Add(vacancy);
        }
    }

    private async void LoadJobSeekers()
    {
        var context = App.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // Загрузка вакансий из базы данных
        var jobSeekers = await Task.Run(() => context.JobSeekers.ToList());
        JobSeekers.Clear();

        foreach (var jobSeeker in jobSeekers)
        {
            JobSeekers.Add(jobSeeker);
        }
    }

    private void EditVacancy(Vacancy vacancy)
    {
        var vacancyViewModel = new VacancyViewModel();

        // Если вакансия не пуста, загружаем её данные
        if (vacancy != null)
        {
            vacancyViewModel.LoadVacancyData(vacancy);
        }

        // Навигация с переданным ViewModel
        _navigationService.Navigate(typeof(VacancyWindow), vacancyViewModel);
    }

    private void DeleteVacancy(Vacancy vacancy)
    {
            try
            {
                _context.Entry(vacancy).State = EntityState.Deleted;
                _context.SaveChanges();
                Vacancies.Remove(vacancy);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                MessageBox.Show($"Error deleting vacancy: {ex.Message}");
            }
    }

    private void DeleteJobSeeker(JobSeeker jobSeeker)
    {
        try
        {
            _context.Entry(jobSeeker).State = EntityState.Deleted;
            _context.SaveChanges();
            JobSeekers.Remove(jobSeeker);
        }
        catch (Exception ex)
        {
            // Логирование ошибки
            MessageBox.Show($"Error deleting vacancy: {ex.Message}");
        }
    }

    private void EditJobSeeker(JobSeeker jobSeeker)
    {
        var jobSeekerViewModel = new JobSeekerViewModel();

        // Если вакансия не пуста, загружаем её данные
        if (jobSeeker != null)
        {
            jobSeekerViewModel.LoadJobSeekerData(jobSeeker);
        }

        // Навигация с переданным ViewModel
        _navigationService.Navigate(typeof(JobSeekerWindow), jobSeekerViewModel);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
