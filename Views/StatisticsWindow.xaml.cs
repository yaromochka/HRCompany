using Microsoft.Extensions.DependencyInjection;
using SoftwareCompanyApp.Models;
using SoftwareCompanyApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace SoftwareCompanyApp.Views
{
    public partial class StatisticsWindow : Page
    {
        public StatisticsWindow()
        {
            InitializeComponent();
            var _statisticsViewModel = App.ServiceProvider.GetRequiredService<StatisticsViewModel>();
            // Установка DataContext
            DataContext = _statisticsViewModel;
        }
    }
}
