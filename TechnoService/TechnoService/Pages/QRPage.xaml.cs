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
using QRCoder;
using System.IO;
using System.Windows.Media.Animation;

namespace TechnoService.Pages
{
    /// <summary>
    /// Логика взаимодействия для QRPage.xaml
    /// </summary>
    public partial class QRPage : Page
    {
        public QRPage()
        {
            InitializeComponent();
            GenerateQR();
        }

        private void GenerateQR()
        {
            string url = "https://forms.gle/EvKo5TpFNVowjHGa9";

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Введите текст для генерации QR-кода", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Генерация QR-кода
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                using (var qrCodeBitmap = qrCode.GetGraphic(10))
                using (var memory = new MemoryStream())
                {
                    qrCodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                    memory.Position = 0;

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    // Отображение QR-кода
                    QRCodeImage.Source = bitmapImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при генерации QR-кода: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
