using SoftwareCompanyApp.ViewModels;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace SoftwareCompanyApp.Views
{
    public partial class VacancyWindow : Page
    {

        public VacancyWindow()
        {
            InitializeComponent();
            // �������� VacancyViewModel ����� DI ���������
            var vacancyViewModel = App.ServiceProvider.GetRequiredService<VacancyViewModel>();
            DataContext = vacancyViewModel;
        }
    }
}
