using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Логика взаимодействия для EquipmentTypes.xaml
    /// </summary>
    public partial class EquipmentTypes : Page
    {
        private readonly Entities db;
        public EquipmentTypes()
        {
            InitializeComponent();
            db = new Entities();
            DataGridEquipmentsTypes.ItemsSource = db.EquipmentTypes.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TypeAddPage());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridEquipmentsTypes.SelectedItem != null)
            {
                EquipmentType selectedEquipmentType = DataGridEquipmentsTypes.SelectedItem as EquipmentType;
                var result = MessageBox.Show("Вы действительно хотите удалить данный тип техники?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.EquipmentTypes.Attach(selectedEquipmentType);
                        db.EquipmentTypes.Remove(selectedEquipmentType);
                        db.SaveChanges();
                        MessageBox.Show("Тип удалён");
                        DataGridEquipmentsTypes.ItemsSource = db.EquipmentTypes.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Тип не был удалён");
                }
            }
            else
            {
                MessageBox.Show("Выберите тип для удаления");
            }
        }
    }
}
