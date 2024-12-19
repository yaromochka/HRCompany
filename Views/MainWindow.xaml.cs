using Microsoft.Extensions.DependencyInjection;
using SoftwareCompany.Helpers;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using System.Windows.Controls;


namespace SoftwareCompanyApp.Views
{
    public partial class MainWindow : Page
    {
        private VacancyViewModel vacancyViewModel;
        public MainWindow(Frame MainFrame)
        {
            InitializeComponent();

            var navigationService = new NavigationService(MainFrame);
            var dbContext = App.ServiceProvider.GetService<ApplicationDbContext>();
            var mainViewModel = new MainViewModel(navigationService, vacancyViewModel, dbContext);
            DataContext = mainViewModel;
        }
    }

}