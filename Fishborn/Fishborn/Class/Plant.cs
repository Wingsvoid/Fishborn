using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishborn
{
    class Plant
    {
        private Point position;
        private int id;


        public int Id { get => id; }
        public Point Pos { get => position; }
        public Plant(int _id, Point _pos)
        {
            id = _id;
            position = _pos;
        }
    }
}
