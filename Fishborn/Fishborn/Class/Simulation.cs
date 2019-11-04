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



        }

        private void FishMovement(Fish fish, double _time)
        {
            Point pos = fish.Pos;
            Point dest = fish.Destination;
            double FullOffsetX;
            double FullOffsetY;
            double PartOffsetX;
            double PartOffsetY;
        }
        private void CreateRandomGeneration()
        {
            Generation generation = new Generation(generations.Count);
            for (int i=0; i<fishNumber; i++)
            {
                double speed = random.Next(1, 10);
                double visibility = random.Next(1, 10);
                double hungertime = random.Next(1, 10);
                Fish fish = new Fish(generation.Id, generation.Id + i, speed, visibility, hungertime, RandomPoint());
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
