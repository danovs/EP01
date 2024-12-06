using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechnoService;

namespace TechnoService.Pages
{
    /// <summary>
    /// Логика взаимодействия для RequestAddPage.xaml
    /// </summary>
    public partial class RequestAddPage : Page
    {

        private readonly Entities db;
        private readonly Request currentRequest;
        public RequestAddPage(Request selectedRequest = null)
        {
            InitializeComponent();
            db = new Entities();
            LoadAllComboBoxes();
            currentRequest = selectedRequest;

            if (currentRequest != null)
            {
                Clients.SelectedValue = currentRequest.Client;
                Equipments.SelectedValue = currentRequest.Equipment;
                Faults.SelectedValue = currentRequest.FaultType;
                Priorities.SelectedValue = currentRequest.Priority;
                Stasuses.SelectedValue = currentRequest.Status;
                Employees.SelectedValue = currentRequest.Employee;
            }
        }


        private void LoadAllComboBoxes()
        {
            var clients = db.Clients.ToList();
            var equipments = db.Equipments.ToList();
            var faults = db.FaultTypes.ToList();
            var priority = db.Priorities.ToList();
            var status = db.Statuses.ToList();
            var employees = db.Employees.ToList();

            Clients.ItemsSource = clients;
            Clients.DisplayMemberPath = "FullName";
            Clients.SelectedValuePath = "ID";

            Equipments.ItemsSource = equipments;
            Equipments.DisplayMemberPath = "Name";
            Equipments.SelectedValuePath = "ID";

            Faults.ItemsSource = faults;
            Faults.DisplayMemberPath = "Type";
            Faults.SelectedValuePath = "ID";

            Priorities.ItemsSource = priority;
            Priorities.DisplayMemberPath = "Priority1";
            Priorities.SelectedValuePath = "ID";

            Stasuses.ItemsSource = status;
            Stasuses.DisplayMemberPath = "Status1";
            Stasuses.SelectedValuePath = "ID";

            Employees.ItemsSource = employees;
            Employees.DisplayMemberPath = "FullName";
            Employees.SelectedValuePath = "ID";
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            Clients = null;
            Equipments = null;
            Faults = null;
            Priorities = null;
            Stasuses = null;
            Employees = null;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение выбранных значений ID
                int? selectedClientId = Clients.SelectedValue as int?;
                int? selectedEquipmentId = Equipments.SelectedValue as int?;
                int? selectedFaultId = Faults.SelectedValue as int?;
                int? selectedPriorityId = Priorities.SelectedValue as int?;
                int? selectedStatusId = Stasuses.SelectedValue as int?;
                int? selectedEmployeeId = Employees.SelectedValue as int?;

                // Проверяем, заполнены ли все обязательные поля
                if (selectedClientId == null || selectedEquipmentId == null || selectedFaultId == null ||
                    selectedPriorityId == null || selectedStatusId == null || selectedEmployeeId == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля");
                    return;
                }

                if (currentRequest == null)
                {
                    // Создание новой заявки
                    var newRequest = new Request
                    {
                        Client = selectedClientId.Value,
                        Equipment = selectedEquipmentId.Value,
                        FaultType = selectedFaultId.Value,
                        Priority = selectedPriorityId.Value,
                        Status = selectedStatusId.Value,
                        Employee = selectedEmployeeId.Value,
                        RequestDate = DateTime.Now // Добавление текущей даты
                    };
                    db.Requests.Add(newRequest);
                    db.SaveChanges();

                    // Добавление записи в RequestLog
                    var newLog = new RequestLog
                    {
                        RequestID = newRequest.ID,
                        LogDate = DateTime.Now,
                        Description = $"Создана новая заявка с ID {newRequest.ID}."
                    };
                    db.RequestLogs.Add(newLog);
                    db.SaveChanges();

                    MessageBox.Show("Заявка добавлена и записана в лог.");
                }
                else
                {
                    // Поиск заявки в базе данных по её ID
                    var requestToUpdate = db.Requests.FirstOrDefault(r => r.ID == currentRequest.ID);
                    if (requestToUpdate == null)
                    {
                        MessageBox.Show("Заявка для обновления не найдена!");
                        return;
                    }

                    // Обновление полей заявки
                    requestToUpdate.Client = selectedClientId.Value;
                    requestToUpdate.Equipment = selectedEquipmentId.Value;
                    requestToUpdate.FaultType = selectedFaultId.Value;
                    requestToUpdate.Priority = selectedPriorityId.Value;
                    requestToUpdate.Status = selectedStatusId.Value;
                    requestToUpdate.Employee = selectedEmployeeId.Value;

                    db.SaveChanges();

                    // Добавление записи в RequestLog
                    var updateLog = new RequestLog
                    {
                        RequestID = requestToUpdate.ID,
                        LogDate = DateTime.Now,
                        Description = $"Заявка с ID {requestToUpdate.ID} была обновлена."
                    };
                    db.RequestLogs.Add(updateLog);
                    db.SaveChanges();

                    MessageBox.Show("Данные заявки обновлены и записаны в лог.");
                }

                // Возврат на предыдущую страницу
                NavigationService.Navigate(new Requests());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

    }
}
