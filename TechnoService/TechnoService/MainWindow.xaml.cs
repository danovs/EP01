using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TechnoService
{

    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

            int UserPositionID = ((App)Application.Current).CurrentUserPositionID;

            FrameManager.MainFrame = MainFrame;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = $"TechnoService - {page.Title}";
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new Uri("Pages/Employees.xaml", UriKind.Relative));
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }

        private void Equipment_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new Uri("Pages/Equipments.xaml", UriKind.Relative));
        }

        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new Uri("Pages/Requests.xaml", UriKind.Relative));
        }

        private void Logs_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new Uri("Pages/RequestLogs.xaml", UriKind.Relative));
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new Uri("Pages/Reports.xaml", UriKind.Relative));
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new Uri("Pages/QRPage.xaml", UriKind.Relative));
        }
    }
}
