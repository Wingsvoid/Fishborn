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
        private double gameSpeed;
        private int stageCount;
        private int survCount;
        private int stageId;
        public List<Fish> Fishes;
        public List<Plant> Plants;
        private List<Generation> generations;
        private Random random;
        private double stageTime;
        private double maxStageTime;
        private bool isProgress;
        public int NowStage { get => stageId; }
        public double StageTime { get => stageTime; }
        public double MaxStageTime { get => maxStageTime; }
        public double SurviversCount { get => survCount; }
        public bool IsProgress { get => isProgress; }
        public List<Generation> Generations { get => generations; }
        public Simulation(double _fieldSizeX, double _fieldSizeY,int _survCount, int _stageCount,  double _maxStageTime, double _gameSpeed)
        {
            random = new Random();
            generations = new List<Generation>();
            Plants = new List<Plant>();
            fieldSizeX = _fieldSizeX;
            fieldSizeY = _fieldSizeY;
            gameSpeed = _gameSpeed;
            maxStageTime = _maxStageTime*60*1000;
            stageCount = _stageCount;
            survCount = _survCount;
            CreateNewGeneration();
            stageTime = 0;
            stageId = 0;
            isProgress = true;
        }
        public void Update(double time)
        {
            time *= gameSpeed;
            stageTime += time;
            if ((stageTime >= maxStageTime) || (Fishes.FindAll(x => x.isAlive).Count <=survCount))
            {
                //foreach (Fish fish in Fishes)
                //{
                //    if (fish.isAlive)
                //    {
                //        fish.isSurvived = true;
                //    }
                //    else
                //        fish.isSurvived = false;
                //}
                stageTime = 0;
                stageId++;
                if (stageId == stageCount)
                {
                    foreach (Fish fish in generations[generations.Count-1].Fishes)
                    {
                        if (fish.isAlive && fish.IsAltruistic)
                        {
                            generations[generations.Count - 1].AltruistsSurvive++;
                        }
                        else if (fish.isAlive && !fish.IsAltruistic)
                        {
                            generations[generations.Count - 1].EgoistsSurvive++;
                        }
                    }
                    isProgress = false;
                    return;
                }
                Plants.Clear();
                CreateNewGeneration();
            }
            if (stageTime/2000 >= Plants.Count)
            {
                CreatePlant();
            }
            foreach (Fish fish in Fishes)
            {
                fish.UpTime(time);
                if (fish.isAlive)
                {
                    
                    foreach (Plant plant in Plants)
                    {
                        if (plant.isActive)
                        {
                            double distanceToPlant = CalculateLength(new Vector(plant.Pos.X - fish.Pos.X, plant.Pos.Y - fish.Pos.Y));
                            if (distanceToPlant <= fish.Visibility && distanceToPlant <= CalculateLength(fish.Direction))
                            {
                                fish.SetDestination(plant.Pos);
                                if (distanceToPlant == 0)
                                {
                                    fish.EatPlant();
                                    plant.Disable();
                                    fish.SetDestination(RandomPoint());
                                }
                            }
                        }
                    }
                    FishMovement(fish, fish.Speed * (time / 1000) * 10);
                }
                else
                {
                    fish.SetDestination(new Point(fish.Pos.X, 0));
                    FishMovement(fish, 5 * (time / 1000) * 10);
                    
                }
            }
        }

        private void FishMovement(Fish fish, double speed)
        {
            Vector pos = new Vector(fish.Pos.X, fish.Pos.Y);
            Vector dest = new Vector(fish.Destination.X, fish.Destination.Y);
            Vector normDir = fish.Direction;
            normDir.Normalize();
            Vector step = Vector.Multiply(normDir, speed);

            if (CalculateLength(Vector.Subtract(dest, pos)) <= CalculateLength(step))
            {
                fish.SetPosition(fish.Destination);
                if (fish.isAlive)
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
        private void CreateNewGeneration()
        {
            Generation nextGen;
            if (generations.Count == 0)
                nextGen = new Generation();
            else
                nextGen = new Generation(generations[generations.Count - 1]);
            foreach(Fish fish in nextGen.Fishes.Where(f => f.GenId==nextGen.Id))
            {
                fish.SetPosition(RandomPoint());
                fish.SetDestination(RandomPoint());
            }
            Fishes = nextGen.Fishes;
            generations.Add(nextGen);       
        }

        private void CreatePlant()
        {
            Plant plant = new Plant(Plants.Count, RandomPoint());
            Plants.Add(plant);
        }
        public Point RandomPoint()
        {
            Point newPoint = new Point(random.Next(0, (int)fieldSizeX), random.Next(0, (int)fieldSizeY));
            return newPoint;
        }
    }
}
