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
        private bool isAltruistic;
        private int id;
        private int genId;
        private double speed;
        private double speedMod;
        private double visibility;
        private double visibilityMod;
        private double hungertime;
        private double hungertimeMod;
        private double starvingtime;
        private double lifetime;
        private bool alive;
        
        public bool IsAltruistic {get => isAltruistic; }
        public int Id { get => id; }
        public int GenId { get => genId; }
        public double Speed { get => speed; }
        public double SpeedMod { get => speedMod; }
        public double Visibility { get => isAltruistic ? AltVisibility() : visibility; }
        public double FullVisibility { get => visibility; }
        public double VisibilityMod { get => visibilityMod; }
        public double Hunger_time { get => hungertime;}
        public double Hunger_timeMod { get => hungertimeMod; }
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
            speedMod = _speed;
            speed = speedMod * 10;
            visibilityMod = _visibility;
            visibility = visibilityMod*350;
            hungertimeMod = _hungertime;
            hungertime = hungertimeMod*70;
            starvingtime = 0;
            lifetime = 0;
            alive = true;
            isAltruistic = AltruistRandom((int)visibility);
        }
        public void UpTime(double time)
        {
            lifetime += time/1000;
            starvingtime += time/1000;
            if (starvingtime >= hungertime)
            {
                Starved();
            }
        }
        public void EatPlant()
        {
            starvingtime = 0;
            alive = true;
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
            SetDestination(new Point(Pos.X, 0));
        }
        private double AltVisibility()
        {
            double altruisticModifier = starvingtime / (hungertime* 0.5);
            double altruisticVis = visibility * altruisticModifier;
            return altruisticModifier < 1 ? altruisticVis : visibility;
        }

        public String ShortInfo()
        {
            string shortInfo ="\n" + "\n" + (isAltruistic ? "A, " : "E, ") +Id + ", " + Math.Round(Hunger_time - StarvingTime);
            return shortInfo;
        }
        public String Info()
        {
            string info = (isAltruistic? "Alt, " : "Ego, ") + "genId: " + genId + " Id: " + id + "\n" + " Spd: " + Math.Round(speed, 1) +
                " Vis: " + Math.Round(Visibility, 1)+ "(" +  Math.Round(visibility, 1) + ")"+ "\n" +  " HTime: " + Math.Round(hungertime, 1) + 
                " LTime: " + Math.Round(lifetime, 1) + "\n" + " isAlive: " + alive;
            return info;
        }
        private bool AltruistRandom(int seed)
        {
            System.Random rand = new Random(seed);
            return rand.Next()%2 == 1 ? true : false;
        }
    }
}
