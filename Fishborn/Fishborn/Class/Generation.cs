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
            System.Random rand = new Random();
            Fishes = new List<Fish>();
            id = 0;
            for (int i = 0; i < 12; i++)
            {
                double speed = rand.Next(10, 100);
                double visibility = rand.Next(10, 100);
                double hungertime = rand.Next(10, 100);
                double summ = speed + visibility + hungertime;
                Fish fish = new Fish(id, id*12 + i, speed / summ, visibility / summ, hungertime / summ, new Point(0, 0));
                fish.SetDestination(new Point(0, 0));
                this.Fishes.Add(fish);
            }
        }
        public Generation(Generation ancestors)
        {
            System.Random rand = new Random();
            Fishes = new List<Fish>();
            id = ancestors.Id + 1;
            for (int i = 0; i < 12; i++)
            {
                double speed = rand.Next(10, 100);
                double visibility = rand.Next(10, 100);
                double hungertime = rand.Next(10, 100);
                double summ = speed + visibility + hungertime;
                Fish fish = new Fish(id, id * 12 + i, speed / summ, visibility / summ, hungertime / summ, new Point(0, 0));
                fish.SetDestination(new Point(0, 0));
                this.Fishes.Add(fish);
            }
        }


    }
}
