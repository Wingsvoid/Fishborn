using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishborn
{
    class Fish
    {
        private Point position;
        private float speed;
        private float visibility;
        private float starvetime;
        private float timelife;

        public float Speed { get => speed; }
        public float Visibility { get => visibility; }
        public float Starve_time { get => starvetime;}
        public float Timelife { get => timelife;}
        public Fish(float _speed, float _visibility, float _starvetime, Point _pos)
        {
            position = _pos;
            speed = _speed;
            visibility = _visibility;
            starvetime = _starvetime;
            timelife = 0;
        }
        public void UpTimelife(float time)
        {
            timelife += time;
        }
    }
}
