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
using System.Windows.Shapes;

namespace Fishborn
{
    /// <summary>
    /// Логика взаимодействия для PlotWindow.xaml
    /// </summary>
    public partial class PlotWindow : Window
    {
        public PlotWindow()
        {
            InitializeComponent();
        }

       

        private void Plotreturn_Click(object sender, RoutedEventArgs e)
        {
            HelloWindow hellowindow = new HelloWindow();
            hellowindow.Show();
            this.Close();
        }

        private void Plotload_Click(object sender, RoutedEventArgs e)
        {
           

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Файлы json|*.json";
            //openFileDialog.InitialDirectory = Environment.CurrentDirectory;
        }
    }
}
