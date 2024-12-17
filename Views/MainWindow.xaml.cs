using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SoftwareCompany.Helpers;
using SoftwareCompanyApp.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace SoftwareCompanyApp.Views
{
    public partial class MainWindow : Page
    {
        public MainWindow(Frame MainFrame)
        {
            InitializeComponent();

            var navigationService = new NavigationService(MainFrame);
            var mainViewModel = new MainViewModel(navigationService);
            DataContext = mainViewModel;
        }
    }

}