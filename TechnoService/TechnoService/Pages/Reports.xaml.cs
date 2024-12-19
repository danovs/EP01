using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TechnoService.Pages
{
    public partial class Reports : Page
    {
        private readonly Entities db;
        private string QCP;
        private int FaultsCount;
        private int RequestCount;

        public Reports()
        {
            InitializeComponent();
            db = new Entities();
            QCP = Quantity.Content.ToString();
        }

        private void Quantity_Click(object sender, RoutedEventArgs e)
        {
            PreparePage();
            Quantity.Visibility = Visibility.Visible;
            QuantityLabel.Content = QCP + " " + db.Requests.Count(x => x.Status == 3).ToString();
        }

        private void AVGTime_Click(object sender, RoutedEventArgs e)
        {
            PreparePage();
            AVGTime.Visibility = Visibility.Visible;

            var requests = db.Requests.ToList();

            int totalDays = 0;
            int completedRequests = 0;

            foreach (var request in requests)
            {
                DateTime startDate = request.RequestDate;
                DateTime endDate;

                if (request.Status == 3) // Статус "выполнено"
                {
                    endDate = db.RequestLogs
                        .Where(log => log.RequestID == request.ID && log.Description.Contains("выполнено"))
                        .OrderByDescending(log => log.LogDate)
                        .Select(log => log.LogDate)
                        .FirstOrDefault();
                }
                else
                {
                    endDate = DateTime.Now;
                }

                if (endDate != DateTime.MinValue)
                {
                    totalDays += (endDate - startDate).Days;
                    completedRequests++;
                }
            }

            float averageTime = completedRequests > 0 ? (float)totalDays / completedRequests : 0;
            AVGTimeLabel.Content = $"Среднее время выполнения заявки: {averageTime:F2} дней";
        }

        private void Faults_Click(object sender, RoutedEventArgs e)
        {
            PreparePage();

            FaultsCount = db.FaultTypes.Count();

            for (int i = 1; i <= FaultsCount; i++)
            {
                FillingContent(i.ToString());
            }
        }

        private void FillingContent(string labelName)
        {
            int id = int.Parse(labelName);
            string FaultTypeName = db.FaultTypes.Where(x => x.ID == id).Select(u => u.Type).FirstOrDefault();
            string FaultTypeCount = db.Requests.Count(x => x.FaultType == id).ToString();

            Label label = new Label()
            {
                Name = "FaultTypeQuantity" + labelName,
                Content = $"{FaultTypeName}: {FaultTypeCount}",
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10)
            };

            Statistic.Children.Add(label);
        }

        private void PreparePage()
        {
            // Очищаем содержимое страницы
            Statistic.Children.Clear();

            // Делаем кнопки видимыми
            Quantity.Visibility = Visibility.Visible;
            AVGTime.Visibility = Visibility.Visible;
            Faults.Visibility = Visibility.Visible;

            // Очищаем все текстовые поля, если это необходимо
            QuantityLabel.Content = string.Empty;
            AVGTimeLabel.Content = string.Empty;
        }
    }
}