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
    /// Логика взаимодействия для RequestLogs.xaml
    /// </summary>
    public partial class RequestLogs : Page
    {

        private readonly Entities db;
        public RequestLogs()
        {
            InitializeComponent();
            db = new Entities();
            DataGridRequests.ItemsSource = db.RequestLogs.ToList();
        }
    }
}
