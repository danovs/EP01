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

namespace TechnoService.Pages
{
    /// <summary>
    /// Логика взаимодействия для Employees.xaml
    /// </summary>
    public partial class Employees : Page
    {
        private readonly Entities db;
        public Employees()
        {
            InitializeComponent();
            db = new Entities();
            DataGridEmployees.ItemsSource = db.Employees.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeesAddPage((sender as Button).DataContext as Employee));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridEmployees.SelectedItem != null)
            {
                // Получаем выбранного сотрудника из DataGrid
                Employee selectedEmployee = DataGridEmployees.SelectedItem as Employee;

                // Переход на страницу редактирования с передачей выбранного сотрудника
                NavigationService.Navigate(new EmployeesAddPage(selectedEmployee));
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridEmployees.SelectedItem != null)
            {
                Employee selectedEmployee = DataGridEmployees.SelectedItem as Employee;
                var result = MessageBox.Show("Вы действительно хотите удалить сотрудника?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Employees.Attach(selectedEmployee);
                        db.Employees.Remove(selectedEmployee);
                        db.SaveChanges();
                        MessageBox.Show("Сотрудник удален");
                        DataGridEmployees.ItemsSource = db.Employees.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении сотрудника.\n" +
                            "Скорее всего, данный сотрудник где-то используется.", ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Сотрудник не был удален из базы данных");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
