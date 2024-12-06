using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace TechnoService.Pages
{

    public partial class ClientsAddPage : Page
    {
        private static readonly Regex FIOregex = new Regex(@"^[А-ЯЁ][а-яё]+$");
        private static readonly Regex PhoneNumberRegex = new Regex(@"^\+?[1-9]\d{9,14}$");

        private readonly Entities db;
        private readonly Client currentClient;

        public ClientsAddPage(Client selectedClient = null)
        {
            InitializeComponent();
            db = new Entities();

            currentClient = selectedClient;

            if (currentClient != null)
            {
                
                FullNameTextBox.Text = $"{currentClient.FirstName} {currentClient.LastName} {currentClient.SecondName}".Trim();
                PhoneNumberTextBox.Text = currentClient.Phone;
                EmailTextBox.Text = currentClient.Email;
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string FIO = FullNameTextBox.Text.Trim();
            string Phone = PhoneNumberTextBox.Text.Trim();
            string Email = EmailTextBox.Text.Trim();

            try
            {

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

                var splitFIO = FIO.Split(' ');

                if (currentClient == null)
                {

                    var newClient = new Client
                    {
                        FirstName = splitFIO[0],
                        LastName = splitFIO.Length > 1 ? splitFIO[1] : string.Empty,
                        SecondName = splitFIO.Length > 2 ? splitFIO[2] : string.Empty,
                        Phone = Phone,
                        Email = Email
                    };

                    db.Clients.Add(newClient);
                    db.SaveChanges();
                    MessageBox.Show("Новый клиент успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var clientToUpdate = db.Clients.SingleOrDefault(c => c.ID == currentClient.ID);

                    if (clientToUpdate != null)
                    {

                        bool isChanged = clientToUpdate.FirstName != splitFIO[0] ||
                                         clientToUpdate.LastName != (splitFIO.Length > 1 ? splitFIO[1] : string.Empty) ||
                                         clientToUpdate.SecondName != (splitFIO.Length > 2 ? splitFIO[2] : string.Empty) ||
                                         clientToUpdate.Phone != Phone ||
                                         clientToUpdate.Email != Email;

                        if (!isChanged)
                        {
                            MessageBox.Show("Вы не внесли никаких изменений. Пожалуйста, измените хотя бы одно поле.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        clientToUpdate.FirstName = splitFIO[0];
                        clientToUpdate.LastName = splitFIO.Length > 1 ? splitFIO[1] : string.Empty;
                        clientToUpdate.SecondName = splitFIO.Length > 2 ? splitFIO[2] : string.Empty;
                        clientToUpdate.Phone = Phone;
                        clientToUpdate.Email = Email;

                        db.SaveChanges();
                        MessageBox.Show("Данные клиента успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Клиент не найден в базе данных. Попробуйте заново.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                FullNameTextBox.Text = string.Empty;
                PhoneNumberTextBox.Text = string.Empty;
                EmailTextBox.Text = string.Empty;
                FrameManager.MainFrame.Navigate(new Uri("/Pages/Clients.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
