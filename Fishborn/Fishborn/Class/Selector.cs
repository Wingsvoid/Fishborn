﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishborn.Class
{
    class Selector
    {
        private List<List<Fish>> pairs;
        private List<Fish> fishes;
        private bool isDebug;
        private Random rand;
        public Selector(List<Fish> fishes, bool debug)
        {
            this.fishes = fishes;
            this.isDebug = debug;
            rand = new Random();
        }
        public List<List<Fish>> GetPairs(int count)
        {
            pairs = new List<List<Fish>>();
            for (int i = 0; i < count; i++)
            {
                Fish parent1 = GetRandomFishFromRating(CalculateRating(fishes));
                var temp = fishes.Where(x => !x.Equals(parent1)).ToList();
                Fish parent2 = GetRandomFishFromRating(CalculateRating(temp));
                List<Fish> pair = new List<Fish>();
                pair.Add(parent1);
                pair.Add(parent2);
                pairs.Add(pair);
            }
            if (isDebug)
            {
                string rating = "";
                foreach (KeyValuePair<Fish, double> kvp in CalculateRating(fishes))
                {
                    rating += string.Format("Key = {0}, Value = {1}",
                        kvp.Key.Id, kvp.Value) + Environment.NewLine;
                }
                foreach (List<Fish> pair in pairs)
                {
                    rating += string.Format("Key = {0}, Value = {1}",
                        pair[0].Id, pair[1].Id) + Environment.NewLine;
                }
                System.Windows.MessageBox.Show(rating);
            }
            return pairs;
        }

        private Dictionary<Fish, double> CalculateRating(List<Fish> fishes)
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
            return rating;
        }

        private Fish GetRandomFishFromRating(Dictionary<Fish, double> rating)
        {
            List<double> probs = new List<double>();
            double total = 0;
            double checkedArea = 0;
            for (int i = 0; i < rating.Count; i++)
            {
                probs.Add(rating.ElementAt(i).Value);
                total += rating.ElementAt(i).Value;
            }

            int index = probs.Count - 1;

            double randPoint = rand.NextDouble() * total;

            for (int i = 0; i < probs.Count; i++)
            {
                checkedArea += probs[i];
                if (randPoint < checkedArea)
                {
                    index = i;
                    break;
                }
            }
            return rating.ElementAt(index).Key;
        }

    }
}
