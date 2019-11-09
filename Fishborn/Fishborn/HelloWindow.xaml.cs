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
using Fishborn.Class;

namespace Fishborn
{
    /// <summary>
    /// Логика взаимодействия для HelloWindow.xaml
    /// </summary>
    public partial class HelloWindow : Window
    {
             
        

        public HelloWindow()
        {
            InitializeComponent();

        }


        


        private void Hellostart_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void Hellostastics_Click(object sender, RoutedEventArgs e)
        {
            PlotWindow plotwindow = new PlotWindow();
            plotwindow.Show();
            this.Close();
        }

        private void Helloexit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
