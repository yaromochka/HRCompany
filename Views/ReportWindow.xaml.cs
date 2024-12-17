using System.Windows.Controls;
using System.Windows;

namespace SoftwareCompanyApp.Views
{
    public partial class ReportWindow : Page
    {
        public ReportWindow()
        {
            InitializeComponent();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement PDF export
            MessageBox.Show("PDF export will be implemented in future versions", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //private void CloseButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}
    }
}
