using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftwareCompany.Helpers;
using SoftwareCompanyApp;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Helpers;
using SoftwareCompanyApp.Models;
using SoftwareCompanyApp.Services;
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

    public ICommand EditVacancyCommand { get; set; }
    public ICommand DeleteVacancyCommand { get; set; }
    public ICommand FindMatchingCandidatesCommand { get; set; }

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
        Vacancies = _vacancyService.Vacancies;

        // Инициализация команд
        EditVacancyCommand = new RelayCommand(param => EditVacancy(param as Vacancy));
        DeleteVacancyCommand = new RelayCommand(param => DeleteVacancy(param as Vacancy));
        FindMatchingCandidatesCommand = new RelayCommand(param => FindCandidates(param as Vacancy));

        // Загрузить вакансии
        LoadVacancies();
    }


    private async void LoadVacancies()
    {
        // Загрузка вакансий из базы данных
        var vacancies = await Task.Run(() => _context.Vacancies.ToList());
        Vacancies.Clear();

        foreach (var vacancy in vacancies)
        {
            Vacancies.Add(vacancy);
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

    private void FindCandidates(Vacancy vacancy)
    {
        if (vacancy != null)
        {
            // Логика поиска кандидатов
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
