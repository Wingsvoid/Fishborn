using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishborn.Class
{
    class Generation
    {
        public List<Fish> Fishes;
        private int id;
        public int Id { get => id; }
        public Generation(int _id)
        {
            id = _id;
            Fishes = new List<Fish>();
        }
        public void SetFish (Fish _fish)
        {
            Fishes.Add(_fish);
        }

    }
}
