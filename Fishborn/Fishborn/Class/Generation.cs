using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fishborn.Class
{
    class Generation
    {
        public List<Fish> Fishes;
        private int id;
        public int Id { get => id; }
        public Generation ()
        {
            
            Fishes = new List<Fish>();
            id = 0;
            for (int i = 0; i < 10; i++)
            {
                Fishes.Add(NewFish(i));
            }
        }
        public Generation(Generation ancestors)
        {
            id = ancestors.Id + 1;
            Fishes = new List<Fish>();
            Selector selector = new Selector(ancestors.Fishes, debug:false);
            Cross cross = new Cross();
            List<List<Fish>> parents;
            List<Fish> survivers = new List<Fish>();
            List<Fish> childrens = new List<Fish>();


            foreach (Fish fish in ancestors.Fishes.Where(x => x.isAlive))
                    survivers.Add(fish);

            parents = selector.GetPairs(10 - survivers.Count);
            foreach(List<Fish> pair in parents)
            {
                List<double> childParams = cross.BreedResult(pair[0], pair[1]);
                try
                {
                    childrens.Add(NewFish(childrens.Count(), childParams[0], childParams[1], childParams[2]));
                }
                catch (IndexOutOfRangeException e)
                {
                    MessageBox.Show("Ащибка");
                }
            }

            Fishes.AddRange(survivers);
            Fishes.AddRange(childrens);


        }
        private Fish NewFish(int i)
        {
            System.Random rand = new Random(i);
            double speed = rand.Next(10, 100);
            double visibility = rand.Next(10, 100);
            double hungertime = rand.Next(10, 100);
            double summ = speed + visibility + hungertime;
            Fish fish = new Fish(id, id * 10 + i, speed / summ, visibility / summ, hungertime / summ, new Point(0, 0));
            fish.SetDestination(new Point(0, 0));
            return fish;
        }
        private Fish NewFish(int i, double speed, double visibility, double hungertime)
        {

            Fish fish = new Fish(id, id * 10 + i, speed, visibility, hungertime, new Point(0, 0));
            fish.SetDestination(new Point(0, 0));
            return fish;
        }


    }
}
