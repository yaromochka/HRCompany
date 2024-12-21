using Microsoft.Extensions.DependencyInjection;
using SoftwareCompanyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareCompanyApp.Views
{
    /// <summary>
    /// Логика взаимодействия для OneVacancyPage.xaml
    /// </summary>
    public partial class OneVacancyPage : Page
    {
        public OneVacancyPage()
        {
            InitializeComponent();
            var _oneVacancyViewModel = App.ServiceProvider.GetRequiredService<OneVacancyViewModel>();
            DataContext = _oneVacancyViewModel;
        }
    }
}
