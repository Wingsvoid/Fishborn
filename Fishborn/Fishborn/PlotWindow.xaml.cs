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
        Dictionary<Point, Ellipse> speedMaxPoints;
        Dictionary<Point, Ellipse> speedMinPoints;
        Dictionary<Point, Ellipse> speedAveragePoints;

        Dictionary<Point, Ellipse> visibilityMaxPoints;
        Dictionary<Point, Ellipse> visibilityMinPoints;
        Dictionary<Point, Ellipse> visibilityAveragePoints;

        Dictionary<Point, Ellipse> hungerMaxPoints;
        Dictionary<Point, Ellipse> hungerMinPoints;
        Dictionary<Point, Ellipse> hungerAveragePoints;

        List<GenerationStat> statistics;

        public PlotWindow()
        {
            InitializeComponent();
            speedMaxPoints = new Dictionary<Point, Ellipse>();
              speedMinPoints = new Dictionary<Point, Ellipse>();
            speedAveragePoints = new Dictionary<Point, Ellipse>();
            visibilityMaxPoints = new Dictionary<Point, Ellipse>();
            visibilityMinPoints = new Dictionary<Point, Ellipse>();
            visibilityAveragePoints = new Dictionary<Point, Ellipse>();
            hungerMaxPoints = new Dictionary<Point, Ellipse>();
            hungerMinPoints = new Dictionary<Point, Ellipse>();
            hungerAveragePoints = new Dictionary<Point, Ellipse>();
            statistics = new List<GenerationStat>();
            DrawGraphic();
        }

        private double XCoordToXPos(int x)
        {
            double xPos = 38.5 * x;

            return xPos;
        }
        private double YCoordToYPos(double y)
        {
            double height = 341;
            
            double yPos = height - height*y*1.5;
            return yPos;
        }
        private Ellipse AddPoint(bool full, string parameter, double yPos, double xPos)
        {
            Ellipse newPoint = new Ellipse();
            Canvas thatCanvas = RedPlot;
            SolidColorBrush myBrush = Brushes.Black;
            //SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            //mySolidColorBrush.Color = Brushes.Red;
            newPoint.StrokeThickness = 2;
            newPoint.Height = 10;
            newPoint.Width = 10;
            switch (parameter)
            {
                case "speed":
                    thatCanvas = RedPlot;
                    myBrush = Brushes.Red;
                    break;
                case "visibility":
                    thatCanvas = GreenPlot;
                    myBrush = Brushes.LimeGreen;
                    break;
                case "hunger":
                    thatCanvas = YellowPlot;
                    myBrush = Brushes.Yellow;
                    break;
                default:
                    break;
            }
            if (full)
            {
                newPoint.Fill = myBrush;
            }
            else
            {
                newPoint.Stroke = myBrush;
            }

            thatCanvas.Children.Add(newPoint);
            newPoint.RenderTransform = new TranslateTransform(xPos, yPos);
            return newPoint;
        }
        private void AddLine(bool solid, string parameter, Point p1, Point p2)
        {
            Canvas thatCanvas = RedPlot;
            SolidColorBrush myBrush = Brushes.Black;
            Line myLine = new Line();
            switch (parameter)
            {
                case "speed":
                    thatCanvas = RedPlot;
                    myBrush = Brushes.Red;
                    break;
                case "visibility":
                    thatCanvas = GreenPlot;
                    myBrush = Brushes.LimeGreen;
                    break;
                case "hunger":
                    thatCanvas = YellowPlot;
                    myBrush = Brushes.Yellow;
                    break;
                default:
                    break;
            }
            myLine.Stroke = myBrush;
            myLine.X1 = p1.X+5;
            myLine.X2 = p2.X+5;
            myLine.Y1 = p1.Y+5;
            myLine.Y2 = p2.Y+5;
            //myLine.HorizontalAlignment = HorizontalAlignment.Left;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            thatCanvas.Children.Add(myLine);

        }
        private void DrawGraphic()
        {
            foreach(GenerationStat stat in statistics)
            {
                speedMaxPoints.Add(new Point(XCoordToXPos(stat.GenerationID), YCoordToYPos(stat.SpeedMax)),
                    AddPoint(false, "speed", YCoordToYPos(stat.SpeedMax), XCoordToXPos(stat.GenerationID)));
                speedMinPoints.Add(new Point(XCoordToXPos(stat.GenerationID), YCoordToYPos(stat.SpeedMin)), 
                    AddPoint(false, "speed", YCoordToYPos(stat.SpeedMin), XCoordToXPos(stat.GenerationID)));
                speedAveragePoints.Add(new Point(XCoordToXPos(stat.GenerationID), YCoordToYPos(stat.SpeedAverage)), 
                    AddPoint(true, "speed", YCoordToYPos(stat.SpeedAverage), XCoordToXPos(stat.GenerationID)));

                visibilityMaxPoints.Add(new Point(XCoordToXPos(stat.GenerationID), YCoordToYPos(stat.VisibilityMax)), 
                    AddPoint(false, "visibility", YCoordToYPos(stat.VisibilityMax), XCoordToXPos(stat.GenerationID)));
                visibilityMinPoints.Add(new Point(XCoordToXPos(stat.GenerationID), YCoordToYPos(stat.VisibilityMin)), 
                    AddPoint(false, "visibility", YCoordToYPos(stat.VisibilityMin), XCoordToXPos(stat.GenerationID)));
                visibilityAveragePoints.Add(new Point(XCoordToXPos(stat.GenerationID), YCoordToYPos(stat.VisibilityAverage)), 
                    AddPoint(true, "visibility", YCoordToYPos(stat.VisibilityAverage), XCoordToXPos(stat.GenerationID)));

                hungerMaxPoints.Add(new Point(XCoordToXPos(stat.GenerationID), YCoordToYPos(stat.HungerMax)), 
                    AddPoint(false, "hunger", YCoordToYPos(stat.HungerMax), XCoordToXPos(stat.GenerationID)));
                hungerMinPoints.Add(new Point(XCoordToXPos(stat.GenerationID), YCoordToYPos(stat.HungerMin)), 
                    AddPoint(false, "hunger", YCoordToYPos(stat.HungerMin), XCoordToXPos(stat.GenerationID)));
                hungerAveragePoints.Add(new Point(XCoordToXPos(stat.GenerationID), YCoordToYPos(stat.HungerAverage)), 
                    AddPoint(true, "hunger", YCoordToYPos(stat.HungerAverage), XCoordToXPos(stat.GenerationID)));
            }
            for (int i=0; i< speedAveragePoints.Count-1; i++)
            {
                AddLine(true, "speed", speedAveragePoints.Keys.ElementAt(i), speedAveragePoints.Keys.ElementAt(i+1));
            }
            for (int i = 0; i < visibilityAveragePoints.Count - 1; i++)
            {
                AddLine(true, "visibility", visibilityAveragePoints.Keys.ElementAt(i), visibilityAveragePoints.Keys.ElementAt(i+1));
            }
            for (int i = 0; i < hungerAveragePoints.Count - 1; i++)
            {
                AddLine(true, "hunger", hungerAveragePoints.Keys.ElementAt(i), hungerAveragePoints.Keys.ElementAt(i+1));
            }



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
                foreach (KeyValuePair<int, GenerationStat> p in allStat)
                {
                    statistics.Add(p.Value);
                }
                if ((statistics.Count() > 0) && (statistics!=null))
                {
                    DrawGraphic();
                }
            }
            else return;

        }

        private void SpeedCB_Checked(object sender, RoutedEventArgs e)
        {
            RedPlot.Visibility = Visibility.Visible;
            RedPlot.UpdateLayout();
        }
        private void SpeedCB_Unchecked(object sender, RoutedEventArgs e)
        {
            RedPlot.Visibility = Visibility.Hidden;
            RedPlot.UpdateLayout();
        }

        private void HungerTCB_Checked(object sender, RoutedEventArgs e)
        {
            YellowPlot.Visibility = Visibility.Visible;
            YellowPlot.UpdateLayout();
        }
        private void HungerTCB_Unchecked(object sender, RoutedEventArgs e)
        {
            YellowPlot.Visibility = Visibility.Hidden;
            YellowPlot.UpdateLayout();
        }

        private void VisibilatyCB_Checked(object sender, RoutedEventArgs e)
        {
            GreenPlot.Visibility = Visibility.Visible;
            GreenPlot.UpdateLayout();
        }

        private void VisibilatyCB_Unchecked(object sender, RoutedEventArgs e)
        {
            GreenPlot.Visibility = Visibility.Hidden;
            GreenPlot.UpdateLayout();
        }
    }
}
