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
        List<Grid> grids;
        List<Rectangle> rectPlants;
        List<TextBlock> textBlocks;

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
        int currentStage;
        List<Fish> listboxSource;


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
            this.KeyDown += new KeyEventHandler(GameScreen_KeyDown);
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
            simulation = new Simulation(Field.Width, Field.Height, (int)Survivor.Value, (int)CountStage.Value,TimeStage.Value,SpeedSim.Value);
            NewStage();
            currentStage = 0;
            dispatcherTimer.Start();
            timeStart = DateTime.Now;
            timePrev = timeStart;
            isStarted = true;
            isPaused = false;
            start.IsEnabled = false;
            Survivor.IsEnabled = false;
            CountStage.IsEnabled = false;
            TimeStage.IsEnabled = false;
            SpeedSim.IsEnabled = false;
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!simulation.IsProgress)
            {
                dispatcherTimer.Stop();
                return;
            }
            timeNext = DateTime.Now;
            double time = (timeNext - timePrev).TotalMilliseconds;
            if (simulation.NowStage!=currentStage)
                NewStage();
            UpdateScreen();                      
            timePrev = timeNext;
            UpdateListBox();
            simulation.Update(time);
        }

        private void NewStage()
        {
            Field.Children.Clear();
            currentStage = simulation.NowStage;
            rectangles = new List<Rectangle>();
            rectPlants = new List<Rectangle>();
            grids = new List<Grid>();
            textBlocks = new List<TextBlock>();
            listboxSource = simulation.Fishes;

            foreach (Fish fish in simulation.Fishes)
            {
                DrawFish(fish);
            }
        }
        private void UpdateScreen()
        {
            foreach (Fish fish in simulation.Fishes)
            {
                Brush image = rectangles[fish.Id%12].Fill.Clone();
                if (fish.Direction.X <= 0)
                {
                    image.RelativeTransform = new ScaleTransform(1, 1, 0.5, 0.5);
                }
                else
                {
                    image.RelativeTransform = new ScaleTransform(-1, 1, 0.5, 0.5);
                }
                if (!fish.isAlive)
                {
                    image.RelativeTransform = new ScaleTransform(1, -1, 0.5, 0.5);
                }
                rectangles[fish.Id%12].Fill = image;

                grids[fish.Id%12].RenderTransform = new TranslateTransform(fish.Pos.X, fish.Pos.Y - 8);
                textBlocks[fish.Id%12].Text = fish.ShortInfo();
            }
            foreach (Plant plant in simulation.Plants)
            {
                if (!plant.isActive)
                    rectPlants[plant.Id].Visibility = Visibility.Collapsed;
            }
            if (simulation.Plants.Count > rectPlants.Count)
            {
                DrawPlant(simulation.Plants[simulation.Plants.Count - 1]);
            }
        }
        private void DrawFish(Fish fish)
        {
            Grid grid = new Grid();
            Rectangle rect = new Rectangle();
            TextBlock text = new TextBlock();
            rect.Height = 16;
            rect.Width = 32;

            if (fish.SpeedMod == Math.Max(fish.SpeedMod, Math.Max(fish.VisibilityMod, fish.Hunger_timeMod)))
            {
                rect.Fill = ib_RedFish;
            }

            if (fish.VisibilityMod == Math.Max(fish.SpeedMod, Math.Max(fish.VisibilityMod, fish.Hunger_timeMod)))
            {
                rect.Fill = ib_GreenFish;
            }

            if (fish.Hunger_timeMod == Math.Max(fish.SpeedMod, Math.Max(fish.VisibilityMod, fish.Hunger_timeMod)))
            {
                rect.Fill = ib_YellowFish;
            }

            if (start.IsEnabled == true)
            {
                start.IsEnabled = false;
            }
            text.Text = fish.ShortInfo();
            text.FontSize = 10;
            textBlocks.Add(text);
            rectangles.Add(rect);
            grid.Children.Add(text);
            grid.Children.Add(rect);

            grids.Add(grid);
            Field.Children.Add(grid);
            grid.RenderTransform = new TranslateTransform(fish.Pos.X, fish.Pos.Y - 8);
        }
        private void DrawPlant(Plant _plant)
        {
            Rectangle rect = new Rectangle();
            rect.Height = 16;
            rect.Width = 32;
            rect.Fill = ib_Plant;
            rectPlants.Add(rect);
            Field.Children.Add(rect);
            rect.RenderTransform = new TranslateTransform(_plant.Pos.X, _plant.Pos.Y);
        }
        private void UpdateListBox()
        {
            ListAllFish.Items.Clear();
            listboxSource = listboxSource.OrderByDescending(o => o.LifeTime).ToList();
            foreach (Fish fish in listboxSource)
            {
                ListAllFish.Items.Add(fish.Info());
            }
        }
        private void restart_Click(object sender, RoutedEventArgs e)
        {
            Field.Children.Clear();
            start.IsEnabled = true;
            dispatcherTimer.Stop();
            isPaused = true;
            Survivor.IsEnabled = true;
            CountStage.IsEnabled = true;
            TimeStage.IsEnabled = true;
            SpeedSim.IsEnabled = true;
            ListAllFish.Items.Clear();

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", " ", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.S:
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
                    break;
                default:
                    //MessageBox.Show();
                    break;
            }

        }

        private void Survivor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                SurvivorLabCenter.Content = Survivor.Value.ToString();
            }
            catch (NullReferenceException)
            {

            }
        }

        private void CountStage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                CountStageCenter.Content = CountStage.Value.ToString();
            }
            catch (NullReferenceException)
            {

            }
        }

        private void TimeStage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                TimeStageCenter.Content = TimeStage.Value.ToString();
            }
            catch (NullReferenceException)
            {

            }
        }

        private void SpeedSim_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                SpeedSimCenter.Content = SpeedSim.Value.ToString();
            }
            catch (NullReferenceException)
            {

            }
        }
    }
}
