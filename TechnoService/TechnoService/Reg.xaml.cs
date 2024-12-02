using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace TechnoService
{

    public partial class Reg : Window
    {
        private static readonly Regex LoginRegex = new Regex(@"^[a-zA-Z0-9]{8,}$");
        private static readonly Regex PasswordRegex = new Regex(@"^(?=.*\d)[a-zA-Z0-9]{8,}$");
        private static readonly Regex FIOregex = new Regex(@"^[А-ЯЁ][а-яё]+$");
        private static readonly Regex PhoneNumberRegex = new Regex(@"^\+?[1-9]\d{9,14}$");

        private bool isNavToAuth = false;
        private readonly Entities db;
        public Reg()
        {
            InitializeComponent();
            db = new Entities();
            LoadPositions();
        }

        private void LoadPositions()
        {
            var positions = db.Positions.ToList();
            Positions.ItemsSource = positions;
            Positions.DisplayMemberPath = "Position1";
            Positions.SelectedValuePath = "ID";
        }


        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Hash_Password(Password.Password);
            string FIO = FIObox.Text;
            string phoneNumber = PhoneNumber.Text;

            try
            {
                if (!LoginRegex.IsMatch(login))
                {
                    MessageBox.Show("Логин должен содержать только английские буквы и цифры, и быть длиной не менее 8 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (db.Users.Any(u => u.Login == login))
                {
                    MessageBox.Show("Пользователь с таким логином уже зарегистрирован в системе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Login.Text = "";
                    return;
                }

                if (!PasswordRegex.IsMatch(password))
                {
                    MessageBox.Show("Пароль должен содержать только английские буквы и цифры, включать минимум одну цифру, и быть длиной не менее 8 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!FIOcheck(FIO))
                {
                    MessageBox.Show("ФИО введено некорректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (db.Employees.Any(u => u.Phone == phoneNumber))
                {
                    MessageBox.Show("Этот номер телефона уже зарегистрирован в системе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                    if (!PhoneNumberRegex.IsMatch(phoneNumber))
                {
                    MessageBox.Show("Введите корректный международный номер телефона (от 10 до 15 символов, допускается знак '+').", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (Positions.SelectedItem == null)
                {
                    MessageBox.Show("Выберите должность.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var selectedPositionID = (int)Positions.SelectedValue;

                var newUser = new User
                {
                    Login = login,
                    Password = password

                };

                db.Users.Add(newUser);
                db.SaveChanges();

                var splitFIO = FIO.Split(' ');

                var newEmployee = new Employee
                {
                    UserID = newUser.ID,
                    PositionID = selectedPositionID,
                    FirstName = splitFIO[0],
                    LastName = splitFIO[1],
                    SecondName = splitFIO.Length > 2 ? splitFIO[2] : string.Empty,
                    Phone = phoneNumber
                };
                db.Employees.Add(newEmployee);
                db.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!");
                isNavToAuth = true;
                Auth auth = new Auth();
                auth.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isNavToAuth)
            {
                return;
            }
            var result = MessageBox.Show("Вы действительно хотите выйтиз из программы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        public static string Hash_Password(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
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
    }
}
