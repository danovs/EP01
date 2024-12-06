using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TechnoService.Pages
{
   
    public partial class Equipments : Page
    {

        private readonly Entities db;
        public Equipments()
        {
            InitializeComponent();
            db = new Entities();
            DataGridEquipments.ItemsSource = db.Equipments.ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridEquipments.SelectedItem != null)
            {
                Equipment selectedEquipment = DataGridEquipments.SelectedItem as Equipment;
                var result = MessageBox.Show("Вы действительно хотите удалить данную технику?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Equipments.Attach(selectedEquipment);
                        db.Equipments.Remove(selectedEquipment);
                        db.SaveChanges();
                        MessageBox.Show("Техника удалена");
                        DataGridEquipments.ItemsSource = db.Equipments.ToList();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении техники.\n" +
                             "Скорее всего, данная техника где-то используется.", ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Техника не была удалена из базы данных");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EquipmentAddPage(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.EquipmentAddPage((sender as Button).DataContext as Equipment));
        }

        private void BtnType_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EquipmentTypes());
        }
    }
}
