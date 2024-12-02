using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace TechnoService
{
    public partial class Auth : Window
    {
        private static readonly Regex LoginRegex = new Regex(@"^[a-zA-Z0-9]{8,}$");
        private static readonly Regex PasswordRegex = new Regex(@"^(?=.*\d)[a-zA-Z0-9]{8,}$");
        private bool isNavToReg = false;
        private readonly Entities db;
        public Auth()
        {
            InitializeComponent();
            db = new Entities();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isNavToReg)
            {
                return;
            }

            var result = MessageBox.Show("Вы действительно хотите выйти из программы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            isNavToReg = true;
            Reg reg = new Reg();
            reg.Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = Hash_Password(PasswordBox.Password);

            try
            {
                if (!LoginRegex.IsMatch(login))
                {
                    MessageBox.Show("Логин должен содержать только английские буквы и цифры, и быть длиной не менее 8 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!PasswordRegex.IsMatch(password))
                {
                    MessageBox.Show("Пароль должен содержать только английские буквы и цифры, включать минимум одну цифру, и быть длиной не менее 8 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден в системе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBox.Show("Вы авторизовались!");
                isNavToReg = true;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string Hash_Password(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
    }
}
