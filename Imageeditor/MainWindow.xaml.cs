using Imageeditor.Extensions;
using Microsoft.Win32;
using System.Drawing;
using System.Windows;

namespace Imageeditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string dialogFilter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
        private Bitmap bitmap = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = dialogFilter;
            var dlgResult = dlg.ShowDialog(this);
            if(dlgResult.HasValue && dlgResult.Value)
            {
                bitmap = new Bitmap(dlg.FileName);
                ImageView.Source = bitmap.ToBitmapSource();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bmp = Processing.Processing.ConverToGray(bitmap);
            ImageView.Source = bmp.ToBitmapSource();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var bmp = Processing.Processing.ConvertToNegative(bitmap);
            ImageView.Source = bmp.ToBitmapSource();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ImageView.Source = bitmap.ToBitmapSource();
        }

        private void BrightNesSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var bmp = Processing.Processing.AdjustBrightness(bitmap, (int)BrightNesSlider.Value);
            ImageView.Source = bmp.ToBitmapSource();
        }

        private void ContrastSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var bmp = Processing.Processing.AdjustContrast(bitmap, ContrastSlider.Value);
            ImageView.Source = bmp.ToBitmapSource();
        }
    }
}
