using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace TechnoService.Pages
{
    public partial class Clients : Page
    {
        private readonly Entities db;
        public Clients()
        {
            InitializeComponent();
            db = new Entities();
            DataGridClients.ItemsSource = db.Clients.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ClientsAddPage((sender as Button).DataContext as Client));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridClients.SelectedItem != null)
            {
                Client selectedClient = DataGridClients.SelectedItem as Client;
                var result = MessageBox.Show("Вы действительно хотите удалить килента с базы данных?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Clients.Attach(selectedClient);
                        db.Clients.Remove(selectedClient);

                        db.SaveChanges();
                        MessageBox.Show("Клиент удалён");
                        DataGridClients.ItemsSource = db.Clients.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении клиента.\n" +
                            "Скорее всего, данный клиент где-то используется.", ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Клиент не был удален из базы данных");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientsAddPage(null));
        }
    }
}
