using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TechnoService.Pages
{

    public partial class EmployeesAddPage : Page
    {
        private static readonly Regex FIOregex = new Regex(@"^[А-ЯЁ][а-яё]+$");
        private static readonly Regex PhoneNumberRegex = new Regex(@"^\+?[1-9]\d{9,14}$");
        private readonly Entities db;
        private readonly Employee currentEmployee;
        public EmployeesAddPage(Employee selectedEmployee = null)
        {
            InitializeComponent();
            db = new Entities();
            currentEmployee = selectedEmployee;
            LoadPositions();

            if (selectedEmployee != null)
            {

                FullNameTextBox.Text = $"{currentEmployee.FirstName} {currentEmployee.LastName} {currentEmployee.SecondName}".Trim();
                PhoneNumberTextBox.Text = currentEmployee.Phone;

                
                if (currentEmployee.Position != null)
                {
                    Positions.SelectedValue = currentEmployee.Position.ID;
                }
            }
        }

        private void LoadPositions()
        {
            var positions = db.Positions.ToList();
            Positions.ItemsSource = positions;
            Positions.DisplayMemberPath = "Position1";
            Positions.SelectedValuePath = "ID";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string FIO = FullNameTextBox.Text.Trim();
            string Phone = PhoneNumberTextBox.Text.Trim();

            
            if (!FIOcheck(FIO))
            {
                MessageBox.Show("ФИО введено некорректно. Убедитесь, что указаны от 2 до 3 слов с заглавной буквы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!PhoneNumberRegex.IsMatch(Phone))
            {
                MessageBox.Show("Введите корректный международный номер телефона (от 10 до 15 символов, допускается знак '+').", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

           
            var selectedPosition = Positions.SelectedItem as Position;  

            if (selectedPosition == null)
            {
                MessageBox.Show("Пожалуйста, выберите роль для сотрудника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            var splitFIO = FIO.Split(' ');

            if (currentEmployee == null)
            {
                
                var newEmployee = new Employee
                {
                    FirstName = splitFIO[0],
                    LastName = splitFIO.Length > 1 ? splitFIO[1] : string.Empty,
                    SecondName = splitFIO.Length > 2 ? splitFIO[2] : string.Empty,
                    Phone = Phone,
                    Position = selectedPosition
                };

                db.Employees.Add(newEmployee); 
                db.SaveChanges();
                MessageBox.Show("Новый сотрудник успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                currentEmployee.FirstName = splitFIO[0];
                currentEmployee.LastName = splitFIO.Length > 1 ? splitFIO[1] : string.Empty;
                currentEmployee.SecondName = splitFIO.Length > 2 ? splitFIO[2] : string.Empty;
                currentEmployee.Phone = Phone;
                currentEmployee.Position = selectedPosition;


                db.SaveChanges();
                MessageBox.Show("Данные сотрудника успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            FullNameTextBox.Text = string.Empty;
            PhoneNumberTextBox.Text = string.Empty;
            Positions.SelectedItem = null;
            NavigationService.GoBack();
        }
        private bool FIOcheck(string FIO)
        {
            var splitFIO = FIO.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (splitFIO.Length < 2 || splitFIO.Length > 3)
            {
                return false;
            }

            foreach (var part in splitFIO)
            {
                if (string.IsNullOrWhiteSpace(part) || !FIOregex.IsMatch(part) || part.Length > 50)
                {
                    return false;
                }
            }
            return true;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
