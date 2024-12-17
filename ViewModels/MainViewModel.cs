using System;
using System.Windows.Input;
using SoftwareCompany.Helpers;
using SoftwareCompanyApp.Helpers;
using SoftwareCompanyApp.Views;

namespace SoftwareCompanyApp.ViewModels
{
    public class MainViewModel
    {
        public ICommand NavigateToStatisticsCommand { get; }
        public ICommand NavigateToVacancyCommand { get; }
        public ICommand NavigateToJobSeekerCommand { get; }

        private readonly NavigationService _navigationService;

        public MainViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

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
        }
    }
}
