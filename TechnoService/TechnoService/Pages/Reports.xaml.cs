using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata;
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

    public partial class Reports : Page
    {
        private readonly Entities db;
        string QCP;
        int FaultsCount;
        int RequestCount;
        public Reports()
        {
            InitializeComponent();
            db = new Entities();
            QCP = Quantity.Content.ToString();
        }

        private void Quantity_Click(object sender, RoutedEventArgs e)
        {
            EmptyPage();
            Quantity.Visibility = Visibility.Visible;
            QuantityLabel.Content = QCP + " " + (db.Requests.Where(x => x.Status == 3).Count().ToString());
        }

        private void AVGTime_Click(object sender, RoutedEventArgs e)
        {
            EmptyPage();
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

            // Вывод среднего времени выполнения заявки
            float averageTime = completedRequests > 0 ? (float)totalDays / completedRequests : 0;
            AVGTimeLabel.Content = $"Среднее время выполнения заявки: {averageTime:F2} дней";
        }

        private void Faults_Click(object sender, RoutedEventArgs e)
        {
            EmptyPage();

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
            string FaultTypeCount = db.Requests.Where(x => x.ID == id).Count().ToString();
            Label label = new Label()
            {
                Name = "FaultTypeQuantity" + labelName,
                Content = FaultTypeName + " " + FaultTypeCount,
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            Statistic.Children.Add(label);
        }

        private void EmptyPage()
        {
            Statistic.Children.Clear();
            Quantity.Visibility = Visibility.Hidden;
            AVGTime.Visibility = Visibility.Hidden;
        }
    }
}
