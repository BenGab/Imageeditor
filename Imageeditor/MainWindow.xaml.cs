using Microsoft.Win32;
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

namespace Imageeditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string dialogFilter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";

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
                BitmapImage img = new BitmapImage(new Uri(dlg.FileName));
                ImageView.Source = img;
            }
        }
    }
}
