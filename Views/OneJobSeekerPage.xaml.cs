using Microsoft.Extensions.DependencyInjection;
using SoftwareCompanyApp.ViewModels;
using System;
using System.Windows.Controls;

namespace SoftwareCompanyApp.Views
{
    public partial class OneJobSeekerPage : Page
    {
        public OneJobSeekerPage()
        {
            InitializeComponent();
            var _oneJobSeekerViewModel = App.ServiceProvider.GetRequiredService<OneJobSeekerViewModel>();
            DataContext = _oneJobSeekerViewModel;
        }
    }
}
