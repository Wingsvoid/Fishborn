using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishborn.Class
{
    class GenerationStat
    {
        private double speedSumm;
        private double visibilitySumm;
        private double hungerSumm;
        private int generationID;
        private int fishCount;
        private double speedMax;
        private double speedMin;
        private double speedAverage;
        private double visibilityMax;
        private double visibilityMin;
        private double visibilityAverage;
        private double hungerMax;
        private double hungerMin;
        private double hungerAverage;
        public GenerationStat()
        {

        }
        public GenerationStat(Generation generation)
        {
            generationID = generation.Id;
            //переменные для расчёта средних значений
            speedSumm = 0;
            visibilitySumm = 0;
            hungerSumm = 0;
            fishCount = 0;
            //начальные значения для поиска максимальный/минимальных параметров
            speedMax = double.MinValue;
            speedMin = double.MaxValue;
            visibilityMax = double.MinValue;
            visibilityMin = double.MaxValue;
            hungerMax = double.MinValue;
            hungerMin = double.MaxValue;
            //подсчёт параметров каждой рыбы
            foreach (Fish fish in generation.Fishes)
            {
                fishCount++;
                speedSumm += fish.SpeedMod;
                visibilitySumm += fish.VisibilityMod;
                hungerSumm += fish.Hunger_timeMod;
                speedMax = Math.Max(speedMax, fish.SpeedMod);
                speedMin = Math.Min(speedMin, fish.SpeedMod);
                visibilityMax = Math.Max(visibilityMax, fish.VisibilityMod);
                visibilityMin = Math.Min(visibilityMin, fish.VisibilityMod);
                hungerMax = Math.Max(hungerMax, fish.Hunger_timeMod);
                hungerMin = Math.Min(hungerMin, fish.Hunger_timeMod);
            }
            speedAverage = speedSumm / fishCount;
            visibilityAverage = visibilitySumm / fishCount;
            hungerAverage = hungerSumm / fishCount;
        }

        public string StatToString()
        {
            string statistic = "";
            statistic = ", speedMax: " + SpeedMax + ", speedMin: " + SpeedMin + ", speedAverage: " + SpeedAverage +
                ", visibilityMax: " + visibilityMax + ", visibilityMin: " + visibilityMin + ", visibilityAverage: " + visibilityAverage +
                ", hungerMax: " + hungerMax + ", hungerMin: " + hungerMin + ", hungerAverage: " + hungerAverage + "\n";
            return statistic;
        }
        public int GenerationID { get => generationID; set => generationID = value; }
        public int FishCount { get => fishCount; set => fishCount = value; }
        public double SpeedMax { get => speedMax; set => speedMax = value; }
        public double SpeedMin { get => speedMin; set => speedMin = value; }
        public double SpeedAverage { get => speedAverage; set => speedAverage = value; }
        public double VisibilityMax { get => visibilityMax; set => visibilityMax = value; }
        public double VisibilityMin { get => visibilityMin; set => visibilityMin = value; }
        public double VisibilityAverage { get => visibilityAverage; set => visibilityAverage = value; }
        public double HungerMax { get => hungerMax; set => hungerMax = value; }
        public double HungerMin { get => hungerMin; set => hungerMin = value; }
        public double HungerAverage { get => hungerAverage; set => hungerAverage = value; }
    }
}
