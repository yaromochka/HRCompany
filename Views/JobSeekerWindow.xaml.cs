using Microsoft.EntityFrameworkCore;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.ViewModels;
using System.Windows.Controls;

namespace SoftwareCompanyApp.Views
{
    public partial class JobSeekerWindow : Page
    {
        private readonly JobSeekerViewModel _viewModel;
        public JobSeekerWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
