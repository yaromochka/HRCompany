using SoftwareCompanyApp.Views;
using System.Windows;

namespace SoftwareCompanyApp
{
    /// <summary>
    /// Логика взаимодействия для MainApp.xaml
    /// </summary>
    public partial class MainApp : Window
    {
        public MainApp()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainWindow(MainFrame));
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            // Переход на предыдущую страницу в Frame
            if (MainFrame.NavigationService.CanGoBack)
            {
                MainFrame.NavigationService.GoBack();
            }
            else
            {
                // Если нет страницы для возврата, закрываем окно
                this.Close();
            }
        }
    }
}
