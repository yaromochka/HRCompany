using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftwareCompany.Helpers;
using SoftwareCompanyApp;
using SoftwareCompanyApp.Data;
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

public class MainViewModel : INotifyPropertyChanged
{
    private readonly ApplicationDbContext _context;
    public ICommand NavigateToStatisticsCommand { get; }
    public ICommand NavigateToVacancyCommand { get; }
    public ICommand NavigateToJobSeekerCommand { get; }

    private readonly NavigationService _navigationService;
    private VacancyService _vacancyService;
    private JobSeekerService _jobSeekerService;

    private ObservableCollection<Vacancy> _vacancies;
    public ObservableCollection<Vacancy> Vacancies
    {
        get => _vacancies;
        set
        {
            if (_vacancies != value)
            {
                _vacancies = value;
                OnPropertyChanged(nameof(Vacancies)); // Уведомляем интерфейс об изменении
            }
        }
    }

    private ObservableCollection<JobSeeker> _jobSeekers;
    public ObservableCollection<JobSeeker> JobSeekers
    {
        get => _jobSeekers;
        set
        {
            if (_jobSeekers != value)
            {
                _jobSeekers = value;
                OnPropertyChanged(nameof(JobSeekers)); // Уведомляем интерфейс об изменении
            }
        }
    }

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

    // Поиск
    private string _vacancySearchText;
    public string VacancySearchText
    {
        get => _vacancySearchText;
        set
        {
            if (_vacancySearchText != value)
            {
                _vacancySearchText = value;
                OnPropertyChanged(nameof(VacancySearchText));
                FilterVacancies();
            }
        }
    }

    private string _jobSeekerSearchText;
    public string JobSeekerSearchText
    {
        get => _jobSeekerSearchText;
        set
        {
            if (_jobSeekerSearchText != value)
            {
                _jobSeekerSearchText = value;
                OnPropertyChanged(nameof(JobSeekerSearchText));
                FilterJobSeekers();
            }
        }
    }

    // Команды
    public ICommand EditVacancyCommand { get; set; }
    public ICommand DeleteVacancyCommand { get; set; }
    public ICommand EditJobSeekerCommand { get; set; }
    public ICommand DeleteJobSeekerCommand { get; set; }
    public ICommand OpenJobSeekerCommand { get; set; }
    public ICommand OpenVacancyCommand { get; set; }

    public MainViewModel(NavigationService navigationService, ApplicationDbContext dbContext)
    {
        _navigationService = navigationService;
        _context = dbContext;

        // Команды для перехода на страницы
        NavigateToStatisticsCommand = new RelayCommand(
            param => _navigationService.Navigate(typeof(StatisticsWindow)),
            param => true);

        NavigateToVacancyCommand = new RelayCommand(
            param => _navigationService.Navigate(typeof(VacancyWindow)),
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

        OpenJobSeekerCommand = new RelayCommand(param => OpenJobSeeker(param as JobSeeker));
        OpenVacancyCommand = new RelayCommand(param => OpenVacancy(param as Vacancy));

        // Загрузить вакансии
        LoadVacancies();
        LoadJobSeekers();
    }

    private void FilterVacancies()
    {
        // Если в поле поиска ничего не написано, показываем все вакансии
        var filteredVacancies = string.IsNullOrWhiteSpace(_vacancySearchText)
            ? _vacancyService.Vacancies.ToList()  // Преобразуем в List для фильтрации
            : _vacancyService.Vacancies
                .Where(v =>
                    v.Title.Contains(_vacancySearchText, StringComparison.OrdinalIgnoreCase) ||
                    v.Company.Contains(_vacancySearchText, StringComparison.OrdinalIgnoreCase) ||
                    v.SalaryFrom.ToString().Contains(_vacancySearchText) ||
                    v.SalaryTo.ToString().Contains(_vacancySearchText)
                )
                .ToList();  // Преобразуем в List после фильтрации

        // Преобразуем обратно в ObservableCollection
        Vacancies = new ObservableCollection<Vacancy>(filteredVacancies);
    }

    private void FilterJobSeekers()
    {
        // Если в поле поиска ничего не написано, показываем всех соискателей
        var filteredJobSeekers = string.IsNullOrWhiteSpace(_jobSeekerSearchText)
            ? _jobSeekerService.JobSeekers.ToList()  // Преобразуем в List для фильтрации
            : _jobSeekerService.JobSeekers
                .Where(js =>
                    js.FirstName.Contains(_jobSeekerSearchText, StringComparison.OrdinalIgnoreCase) ||
                    js.LastName.Contains(_jobSeekerSearchText, StringComparison.OrdinalIgnoreCase) ||
                    js.Position.Contains(_jobSeekerSearchText, StringComparison.OrdinalIgnoreCase) ||
                    js.SalaryFrom.ToString().Contains(_jobSeekerSearchText) ||
                    js.SalaryTo.ToString().Contains(_jobSeekerSearchText) ||
                    js.Location.Contains(_jobSeekerSearchText, StringComparison.OrdinalIgnoreCase)
                )
                .ToList();  // Преобразуем в List после фильтрации

        // Преобразуем обратно в ObservableCollection
        JobSeekers = new ObservableCollection<JobSeeker>(filteredJobSeekers);
    }


    private void OpenJobSeeker(JobSeeker jobSeeker)
    {
        var oneJobSeekerViewModel = new OneJobSeekerViewModel();

        // Если соискатель не пуст, загружаем его данные
        if (jobSeeker != null)
        {
            oneJobSeekerViewModel.LoadJobSeekerData(jobSeeker);
        }
        _navigationService.Navigate(typeof(OneJobSeekerPage), oneJobSeekerViewModel);
    }

    private void OpenVacancy(Vacancy vacancy)
    {
        var oneVacancyViewModel = new OneVacancyViewModel();

        // Если вакансия не пуста, загружаем её данные
        if (vacancy != null)
        {
            oneVacancyViewModel.LoadVacancyData(vacancy);
        }
        _navigationService.Navigate(typeof(OneVacancyPage), oneVacancyViewModel);
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
        // Загрузка соискателей из базы данных
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
            MessageBox.Show($"Error deleting job seeker: {ex.Message}");
        }
    }

    private void EditJobSeeker(JobSeeker jobSeeker)
    {
        var jobSeekerViewModel = new JobSeekerViewModel();

        // Если соискатель не пуста, загружаем его данные
        if (jobSeeker != null)
        {
            jobSeekerViewModel.LoadJobSeekerData(jobSeeker);
        }

        // Навигация с переданным ViewModel
        _navigationService.Navigate(typeof(JobSeekerWindow), jobSeekerViewModel);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
