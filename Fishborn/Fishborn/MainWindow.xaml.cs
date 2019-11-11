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
using System.IO;
using Newtonsoft.Json;
using Fishborn.Class;

namespace Fishborn
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Simulation simulation;

        Dictionary<Fish, Rectangle> rectangles;
        Dictionary<Fish, Grid> grids;
        Dictionary<Plant, Rectangle> rectPlants;
        Dictionary<Fish, TextBlock> textBlocks;

        //кисти
        ImageBrush ib_RedFish;
        ImageBrush ib_YellowFish;
        ImageBrush ib_GreenFish;
        ImageBrush ib_Plant;
        ImageBrush ib_DeadFish;

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
            ib_DeadFish = new ImageBrush();

            //источники изображения
            ib_RedFish.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/RedFish.png", UriKind.Absolute));
            ib_YellowFish.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/YellowFish.png", UriKind.Absolute));
            ib_GreenFish.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/GreenFish.png", UriKind.Absolute));
            ib_Plant.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/Plant.png", UriKind.Absolute));
            ib_DeadFish.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/DeadFish.png", UriKind.Absolute));
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
        private void WriteJSONtoFile(string path, string json)
        {
            File.WriteAllText(@path, json);
        }

        private string ReadJSONfromFile(string path)
        {
            string json = File.ReadAllText(@path);
            return json;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!simulation.IsProgress)
            {
                dispatcherTimer.Stop();

                //генерация названия файла из текущего времени
                string name = DateTime.Now.ToString();
                name = name.Replace(".", "-").Replace(":", "-").Replace(" ", "_");
                string path = string.Format("{0}.json", name);
                //создания словаря, где ID поколения это ключ, а статистика поколения это значение
                Dictionary<int, GenerationStat> allStat = new Dictionary<int, GenerationStat>();
                //для каждого поколения из симуляции считаем статистику и добавляем в словарь
                foreach(Generation generation in simulation.Generations)
                {
                    GenerationStat stat = new GenerationStat(generation);
                    allStat.Add(generation.Id, stat);
                }
                //сериализация словаря в строку
                string json = "";
                json = JsonConvert.SerializeObject(allStat);
                //запись строки в файл
                File.WriteAllText(@path, json);

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
            rectangles = new Dictionary<Fish, Rectangle>();
            rectPlants = new Dictionary<Plant, Rectangle>();
            grids = new Dictionary<Fish, Grid>();
            textBlocks = new Dictionary<Fish, TextBlock>();
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
                Brush image = rectangles[fish].Fill.Clone();
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
                    image = ib_DeadFish;
                    image.RelativeTransform = new ScaleTransform(1, -1, 0.5, 0.5);
                }
                rectangles[fish].Fill = image;

                grids[fish].RenderTransform = new TranslateTransform(fish.Pos.X, fish.Pos.Y - 8);
                textBlocks[fish].Text = fish.ShortInfo();
            }
            foreach (Plant plant in simulation.Plants)
            {
                if (!plant.isActive)
                    rectPlants[plant].Visibility = Visibility.Collapsed;
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
            textBlocks.Add(fish, text);
            rectangles.Add(fish, rect);
            grid.Children.Add(text);
            grid.Children.Add(rect);

            grids.Add(fish, grid);
            Field.Children.Add(grid);
            grid.RenderTransform = new TranslateTransform(fish.Pos.X, fish.Pos.Y - 8);
        }
        private void DrawPlant(Plant _plant)
        {
            Rectangle rect = new Rectangle();
            rect.Height = 16;
            rect.Width = 32;
            rect.Fill = ib_Plant;
            rectPlants.Add(_plant, rect);
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
            MessageBoxResult result = MessageBox.Show("Вернуться в главное меню?", " ", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                HelloWindow hellowindow = new HelloWindow();
                hellowindow.Show();
                this.Close();
            }

            if (result == MessageBoxResult.No)
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
