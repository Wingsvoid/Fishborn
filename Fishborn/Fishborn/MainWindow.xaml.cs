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
        List<Rectangle> rectPlants;

        //кисти
        ImageBrush ib_RedFish;
        ImageBrush ib_YellowFish;
        ImageBrush ib_GreenFish;
        ImageBrush ib_Plant;

        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        DateTime timeStart;
        DateTime timeNext;
        DateTime timePrev;
        //DateTime timeEnd;
        bool isStarted;
        bool isPaused;


        public MainWindow()
        {
            InitializeComponent();

           



            //создание кистей
            ib_RedFish = new ImageBrush();
            ib_YellowFish = new ImageBrush();
            ib_GreenFish = new ImageBrush();
            ib_Plant = new ImageBrush();

            //источники изображения
            ib_RedFish.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/RedFish.png", UriKind.Absolute));
            ib_YellowFish.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/YellowFish.png", UriKind.Absolute));
            ib_GreenFish.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/GreenFish.png", UriKind.Absolute));
            ib_Plant.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/Plant.png", UriKind.Absolute));
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 30);
            isStarted = false;
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted)
            {
                if (isPaused)
                {
                    dispatcherTimer.Start();
                    isPaused = false;
                    timePrev = DateTime.Now;
                }
                else
                {
                    dispatcherTimer.Stop();
                    isPaused = true;
                }
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {

            simulation = new Simulation(Field.Width, Field.Height, 1, 12);
            rectangles = new List<Rectangle>();
            rectPlants = new List<Rectangle>();

            foreach (Fish fish in simulation.Fishes)
            {
                Rectangle rect = new Rectangle();
                rect.Height = 16;
                rect.Width = 32;

                if (fish.Speed == Math.Max(fish.Speed, Math.Max(fish.Visibility, fish.Hunger_time)))
                {
                    rect.Fill = ib_RedFish;
                }

                if (fish.Visibility == Math.Max(fish.Speed, Math.Max(fish.Visibility, fish.Hunger_time)))
                {
                    rect.Fill = ib_GreenFish;
                }

                if (fish.Hunger_time == Math.Max(fish.Speed, Math.Max(fish.Visibility, fish.Hunger_time)))
                {
                    rect.Fill = ib_YellowFish;
                }

                if (start.IsEnabled == true)
                {
                    start.IsEnabled = false;
                }


                rectangles.Add(rect);
                Field.Children.Add(rect);
                rect.RenderTransform = new TranslateTransform(fish.Pos.X, fish.Pos.Y);
            }

            dispatcherTimer.Start();
            timeStart = DateTime.Now;
            timePrev = timeStart;
            isStarted = true;
            isPaused = false;
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timeNext = DateTime.Now;
            double time = (timeNext - timePrev).TotalMilliseconds;
            simulation.Update(time);
            foreach (Fish fish in simulation.Fishes)
            {
                rectangles[fish.Id].RenderTransform = new TranslateTransform(fish.Pos.X, fish.Pos.Y);
                //if (!fish.isAlive)
                //{
                //    rectangles[fish.Id].Fill = dead
                //}
            }
            if (simulation.Plants.Count > rectPlants.Count)
            {
                Rectangle rect = new Rectangle();
                rect.Height = 16;
                rect.Width = 32;
                rect.Fill = ib_Plant;
                rectPlants.Add(rect);
                Field.Children.Add(rect);
                rect.RenderTransform = new TranslateTransform(simulation.Plants[rectPlants.Count - 1].Pos.X, simulation.Plants[rectPlants.Count - 1].Pos.Y);
            }
            timePrev = timeNext;
        }
        private void restart_Click(object sender, RoutedEventArgs e)
        {
            if (restart.IsEnabled == true)
            {
                Field.Children.Clear();
                start.IsEnabled = true;
            }
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
