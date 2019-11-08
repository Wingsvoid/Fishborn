using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishborn.Class
{
    class Selector
    {
        private Dictionary<Fish, Fish> pairs;
        private List<Fish> fishes;
        private int pairsCount;
        public Selector(List<Fish> fishes)
        {
            pairs = new Dictionary<Fish, Fish>();
        }
        public Dictionary<Fish, Fish> GetPairs(int count)
        {
            return pairs;
        }
        public void Temp()
        {
            double sumLivedTime = 0;
            foreach (Fish fish in fishes)
            {
                sumLivedTime += fish.LifeTime;
            }
            Dictionary<Fish, double> rating = new Dictionary<Fish, double>();
            foreach (Fish fish in fishes)
            {
                rating.Add(fish, fish.LifeTime / sumLivedTime);
            }
            Dictionary<Fish, Fish> pairs = new Dictionary<Fish, Fish>();
            //return pairs;
        }
    }
}
