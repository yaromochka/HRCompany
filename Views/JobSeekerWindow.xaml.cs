using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.ViewModels;
using System.Windows.Controls;

namespace SoftwareCompanyApp.Views
{
    public partial class JobSeekerWindow : Page
    {
        public JobSeekerWindow()
        {
            InitializeComponent();
            // Получаем VacancyViewModel через DI контейнер
            var jobSeekerViewModel = App.ServiceProvider.GetRequiredService<JobSeekerViewModel>();
            DataContext = jobSeekerViewModel;
        }
    }
}
