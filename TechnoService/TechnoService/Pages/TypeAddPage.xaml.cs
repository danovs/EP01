using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TechnoService.Pages
{
    public partial class TypeAddPage : Page
    {
        private readonly Entities db;

        public TypeAddPage()
        {
            InitializeComponent();
            db = new Entities();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string typeName = TypeTextBox.Text;

            
            if (string.IsNullOrEmpty(typeName))
            {
                MessageBox.Show("Заполните тип техники");
                return;
            }

            try
            {
                
                var existingType = db.EquipmentTypes
                                      .FirstOrDefault(t => t.Type.Equals(typeName, StringComparison.OrdinalIgnoreCase));

                if (existingType != null)
                {
                    MessageBox.Show("Тип техники с таким именем уже существует!");
                    return;
                }

                
                var newType = new EquipmentType
                {
                    Type = typeName
                };

               
                db.EquipmentTypes.Add(newType);
                db.SaveChanges();

                MessageBox.Show("Тип техники успешно добавлен!");

                TypeTextBox.Clear();
                NavigationService.Navigate(new EquipmentTypes());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении типа техники: {ex.Message}");
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}