using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishborn.Class
{
    class Cross
    {
        Random rand;
        public Cross()
        {
            rand = new Random();
        }
        public List<double> BreedResult(Fish _parent1, Fish _parent2)
        {
            List<double> children = new List<double>(); ;
            List<double> parent1 = ParamsToList(_parent1);
            List<double> parent2 = ParamsToList(_parent2);
            children = Breed(new List<List<double>>() {parent1, parent2 });

            return children;
        }
        private List<double> Mutate(List<double> _params)
        {
            List<double> mutatedParams = _params;

            mutatedParams[rand.Next(3)] *= (rand.NextDouble() + 0.5);
            return mutatedParams;

        }
        private List<double> ParamsToList(Fish fish)
        {
            List<double> modifiers = new List<double>();
            modifiers.Add(fish.SpeedMod);
            modifiers.Add(fish.VisibilityMod);
            modifiers.Add(fish.Hunger_timeMod);
            return modifiers;
        }
        private List<double> Breed(List<List<double>> paramsPair)
        {
            List<double> children = new List<double>();
            int separator = rand.Next(2) +1;

            int xParent = rand.Next(2);
            int yParent = (xParent+1) % 2;

            for (int i=0; i<separator; i++)
            {
                children.Add(paramsPair[xParent][i]);
            }
            for (int i=separator; i< paramsPair[xParent].Count; i++)
            {
                children.Add(paramsPair[yParent][i]);
            }

            if (rand.Next(100)<5)
            {
                children = Mutate(children);
            }

            children = Equalize(children);

            return children;

        }
        private List<double> Equalize(List<double> modifiers)
        {
            List<double> equalized = new List<double>();
            double summ = 0;
            double diff;
            foreach(double mod in modifiers)
            {
                summ += mod;
            }
            if (summ > 1)
            {
                diff = (summ - 1)/3;
                foreach (double mod in modifiers)
                    equalized.Add(mod - diff);
            }
            else if (summ<1)
            {
                diff = (1-summ) / 3;
                foreach (double mod in modifiers)
                    equalized.Add(mod + diff);
            } 
            return equalized;
        }

    }
}
