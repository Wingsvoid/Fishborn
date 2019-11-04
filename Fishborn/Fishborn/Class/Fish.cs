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
        private Point destination;
        private Vector direction;
        private int id;
        private int genId;
        private double speed;
        private double visibility;
        private double hungertime;
        private double starvingtime;
        private double lifetime;
        private bool alive;
        

        public int Id { get => id; }
        public int GenId { get => genId; }
        public double Speed { get => speed; }
        public double Visibility { get => visibility; }
        public double Hunger_time { get => hungertime;}
        public double LifeTime { get => lifetime;}
        public double StarvingTime { get => starvingtime; }
        public Point Pos { get => position; }
        public Vector Direction { get => direction; }
        public Point Destination { get => destination; }
        public bool isAlive { get => alive; }
        public Fish(int _genId, int _id, double _speed, double _visibility, double _hungertime, Point _pos)
        {
            genId = _genId;
            id = _id;
            position = _pos;
            speed = _speed;
            visibility = _visibility;
            hungertime = _hungertime;
            starvingtime = 0;
            lifetime = 0;
            alive = true;
        }
        public void UpTime(double time)
        {
            lifetime += time;
            starvingtime += time;
            if (starvingtime == hungertime)
            {
                Starved();
            }
        }
        public void EatPlant()
        {
            starvingtime = 0;
        }
        public void MoveOffset(double x, double y)
        {
            position.Offset(x, y);
        }
        public void SetPosition(Point newPoint)
        {
            position = newPoint;
        }
        public void SetDestination(Point newPoint)
        {
            destination = newPoint;
            direction = new Vector(destination.X - position.X, destination.Y - position.Y);
        }
        private void Starved()
        {
            alive = false;
        }
    }
}
