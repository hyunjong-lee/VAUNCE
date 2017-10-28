using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public class GameObject
    {
        public GameObject(string rname, float x, float y, string direction)
        {
            this.resourceName = rname;
            pos.X = x;
            pos.Y = y;
            this.direction = direction;
        }

        public string name;
        public string resourceName;
        public string direction;
        public PointF pos;

        public string RID
        {
            get
            {
                return string.Format("{0}_{1}", resourceName, direction);
            }
        }
    }

    public class Alien : GameObject
    {
        float accel = 0.0f;
        public DateTime lastAlive;
        float sx;
        float sy;
        string sd;

        public Alien(float x, float y, string direction) : 
            base("alien", x - Resources.Boxes["alien_up"].X / 2, y - Resources.Boxes["alien_up"].Y / 2, "up")
        {
            sx = x;
            sy = y;
            sd = direction;
            spawn();
        }

        void spawn()
        {
            pos.X = sx;
            pos.Y = sy;
            direction = sd;
            lastAlive = DateTime.Now;
        }

        void jump()
        {
            if (direction == "down") direction = "up";
            else direction = "down";
            Resources.Sounds["jump"].Position = TimeSpan.Zero;
            Resources.Sounds["jump"].Play();
            accel = 0.0f;
        }

        public void tick()
        {
            if (direction == "up")
            {
                pos.Y -= (17.3f + accel);
                if (pos.Y <= 0) jump();
            }
            else if(direction == "down")
            {
                pos.Y += (17.3f + accel);
                if (pos.Y >= Resources.Boxes["bg"].Y - Resources.Boxes[RID].Y) jump();
            }
            else
            {
                spawn();
            }

            accel += 1.0f;
        }
    }

    public class Missile : GameObject
    {
        public Missile(string name, int x, int y, string direction) : base(name, x, y, direction)
        {
        }

        public void flip()
        {
            if (direction == "left") direction = "right";
            else direction = "left";
        }

        public void tick()
        {
            if (direction == "left")
            {
                pos.X -= 17.3f;
                if (pos.X + Resources.Boxes[RID].X <= 0) flip();
            }
            else
            {
                pos.X += 17.3f;
                if (pos.X >= Resources.Boxes["bg"].X + Resources.Boxes[RID].X) flip();
            }
        }

    }

    public class GameState
    {
        public List<Alien> aliens = new List<Alien>();
        public List<Alien> ghosts = new List<Alien>();
        internal List<GameObject> missiles = new List<GameObject>();
        public TimeSpan bestTime = TimeSpan.Zero;

        public GameState()
        {
            aliens.Add(new Alien(Resources.Boxes["bg"].X / 2, Resources.Boxes["bg"].Y / 2, "up"));
            missiles.Add(new Missile("fish", Resources.Boxes["bg"].X, Resources.Boxes["bg"].Y / 2, "left"));
        }

        protected void checkCollision()
        {
            foreach (var alien in aliens)
            {
                if (alien.direction == "die") continue;

                var ax = alien.pos.X;
                var ay = alien.pos.Y;
                foreach (var missile in missiles)
                {
                    var mx = missile.pos.X;
                    var my = missile.pos.Y;
                    var span = 15;

                    if (ax + Resources.Boxes[alien.RID].X <= mx + span) continue;
                    if (ay + Resources.Boxes[alien.RID].Y <= my + span) continue;

                    if (mx + Resources.Boxes[missile.RID].X + span <= ax) continue;
                    if (my + Resources.Boxes[missile.RID].Y + span <= ay) continue;

                    Resources.Sounds["die"].Position = TimeSpan.Zero;
                    Resources.Sounds["die"].Play();
                    alien.direction = "die";
                    var diff = DateTime.Now - alien.lastAlive;
                    bestTime = (bestTime > diff) ? bestTime : diff;
                }
            }
        }

        public void tick()
        {
            foreach (Alien alien in aliens) alien.tick();
            foreach (Missile missile in missiles) missile.tick();
            checkCollision();
        }
    }
}
