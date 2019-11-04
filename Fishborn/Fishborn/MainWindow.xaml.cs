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

namespace Fishborn
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Simulation simulation;

        List<Rectangle> rectangles;

        //кисти
        ImageBrush ib_RedFish;


        public MainWindow()
        {
            InitializeComponent();

            simulation = new Simulation(302, 397, 1, 12);
            rectangles = new List<Rectangle>();


           
            //создание кистей
            ib_RedFish = new ImageBrush();

            //источники изображения
            ib_RedFish.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/RedFish.png", UriKind.Absolute));

        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {

        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            foreach (Fish fish in simulation.Fishes)
            {
                Rectangle rect = new Rectangle();
                rect.Height = 16;
                rect.Width = 32;

                //if (fish.Speed == )
                //{

                //}
                //заполнение кистей
                rect.Fill = ib_RedFish;



                rectangles.Add(rect);
                Field.Children.Add(rect);
                rect.RenderTransform = new TranslateTransform(fish.Pos.X, fish.Pos.Y);
            }

        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", " ", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {                
                this.Close();
            }
        }
    }
}
