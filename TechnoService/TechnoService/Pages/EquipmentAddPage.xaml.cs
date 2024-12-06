using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TechnoService.Pages
{

    public partial class EquipmentAddPage : Page
    {
        private readonly Entities db;
        private readonly Equipment currentEquipment;
        
        public EquipmentAddPage(Equipment selectedEquipment = null)
        {
            InitializeComponent();
            db = new Entities();
            currentEquipment = selectedEquipment;
            LoadTypes();
            if (currentEquipment != null )
            {
                NameTextBox.Text = currentEquipment.Name;
                SerialNumberTextBox.Text = currentEquipment.SerialNumber;
                ModelTextBox.Text = currentEquipment.Model;
                DescTextBox.Text = currentEquipment.TechSpec;

                if (currentEquipment.Type != null)
                {
                    Types.SelectedValue = currentEquipment.EquipmentType.ID;
                }
            }
        }

        private void LoadTypes()
        {
            var types = db.EquipmentTypes.ToList();
            Types.ItemsSource = types;
            Types.DisplayMemberPath = "Type";
            Types.SelectedValuePath = "ID";

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string Name = NameTextBox.Text;
            string SerialNumber = SerialNumberTextBox.Text;
            string Model = ModelTextBox.Text;
            string Desc = DescTextBox.Text;

            if (string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Заполните название техники");
                return;
            }

            if (string.IsNullOrEmpty(SerialNumber))
            {
                MessageBox.Show("Заполните серийный номер техники");
                return;
            }

            if (string.IsNullOrEmpty(Model))
            {
                MessageBox.Show("Заполните модель техники");
                return;
            }

            var selectedType = Types.SelectedItem as EquipmentType;
            if (selectedType == null)
            {
                MessageBox.Show("Выберите тип техники");
                return;
            }

            try
            {
                if (currentEquipment == null)
                {
                    
                    var newEquipment = new Equipment
                    {
                        Name = Name,
                        SerialNumber = SerialNumber,
                        Model = Model,
                        TechSpec = Desc,
                        Type = selectedType.ID
                    };

                    db.Equipments.Add(newEquipment);
                    
                }
                else
                {
                    
                    var existingEquipment = db.Equipments.FirstOrDefault(u => u.ID == currentEquipment.ID);
                    if (existingEquipment != null)
                    {
                        existingEquipment.Name = Name;
                        existingEquipment.SerialNumber = SerialNumber;
                        existingEquipment.Model = Model;
                        existingEquipment.TechSpec = Desc;
                        existingEquipment.Type = selectedType.ID;
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}");
            }
        }
    }
}
