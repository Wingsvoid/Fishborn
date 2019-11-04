using Fishborn.Class;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishborn
{
    class Simulation
    {
        private double fieldSizeX;
        private double fieldSizeY;
        private int gameSpeed;
        private int fishNumber;
        public List<Fish> Fishes;
        public List<Plant> Plants;
        private List<Generation> generations;
        private Random random;
        private double stageTime;
        public double StageTime { get => stageTime; }

        public Simulation(double _fieldSizeX, double _fieldSizeY, int _gameSpeed, int _fishNumber)
        {
            random = new Random();
            generations = new List<Generation>();
            fieldSizeX = _fieldSizeX;
            fieldSizeY = _fieldSizeY;
            gameSpeed = _gameSpeed;
            fishNumber = _fishNumber;
            CreateRandomGeneration();
            Fishes = generations[0].Fishes;
            stageTime = 0;
            
        }
        public void Update(double time)
        {
            stageTime += time;
            foreach (Fish fish in Fishes)
            {
                fish.UpTime(time);
                FishMovement(fish, time);
            }



        }

        private void FishMovement(Fish fish, double _time)
        {
            //_time /= 500;
            Vector pos = new Vector(fish.Pos.X, fish.Pos.Y);
            Vector dest = new Vector(fish.Destination.X, fish.Destination.Y);
            Vector step = Vector.Multiply(fish.Direction, fish.Speed * gameSpeed * (_time/1000)*10);

            if (CalculateLength(Vector.Subtract(dest, pos)) <= CalculateLength(step))
            {
                fish.SetPosition(fish.Destination);
                fish.SetDestination(RandomPoint());
            }
            else
                fish.SetPosition(Vector.Add(step, fish.Pos));
        }
        private double CalculateLength(Vector distVector)
        {
            double length = Math.Sqrt(Math.Pow(distVector.X, 2)+Math.Pow(distVector.Y, 2));
            return length;
        }
        private void CreateRandomGeneration()
        {
            Generation generation;
            if (generations == null)
                generation = new Generation(0);
            else
                generation = new Generation(generations.Count);
            for (int i=0; i<fishNumber; i++)
            {
                double speed = random.Next(1, 10);
                double visibility = random.Next(1, 10);
                double hungertime = random.Next(1, 10);
                Fish fish = new Fish(generation.Id, generation.Id + i, speed, visibility, hungertime, RandomPoint());
                fish.SetDestination(RandomPoint());
                generation.SetFish(fish);
            }
            generations.Add(generation);           
        }

        private void CreatePlant()
        {
            Plant plant = new Plant(Plants.Count, RandomPoint());
            Plants.Add(plant);
        }
        private Point RandomPoint()
        {
            Point newPoint = new Point(random.Next(0, (int)fieldSizeX), random.Next(0, (int)fieldSizeY));
            return newPoint;
        }
    }
}
