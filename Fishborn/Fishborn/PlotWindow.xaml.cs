using Fishborn.Class;
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
using Newtonsoft.Json;
using System.IO;

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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы json|*.json";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                string json = File.ReadAllText(@path);
                var allStat = JsonConvert.DeserializeObject<Dictionary<int, GenerationStat>>(json);               
                string stats = "";
                foreach(KeyValuePair<int, GenerationStat> p in allStat)
                {
                    stats += p.Key + p.Value.StatToString();
                }
                stats += "\n";
                
                MessageBox.Show(stats);
            }
            else return;

        }

        private void SpeedCB_Checked(object sender, RoutedEventArgs e)
        {
            //if (SpeedCB.IsChecked == true)
            //{
            //    RedPlot.Visibility = Visibility.Visible;
            //}
                        
            //else if (SpeedCB.IsChecked == false)
            //{
            //    RedPlot.Visibility = Visibility.Hidden;
            //}
        }

        private void HungerTCB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void VisibilatyCB_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
