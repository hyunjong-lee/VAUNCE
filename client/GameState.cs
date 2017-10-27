using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class GameObject
    {
        public GameObject(string name, int x, int y, string direction)
        {
            this.name = name;
            pos.X = x;
            pos.Y = y;
            this.direction = direction;
        }

        public string name;
        public string direction;
        public Point pos;
        public bool isAlive = true;

        public string RID
        {
            get
            {
                return string.Format("{0}_{1}", name, direction);
            }
        }
    }

    public class Alien : GameObject
    {
        public Alien(int x, int y, string direction) : base("alien",
            x - Resources.Boxes["alien_up"].X / 2,
            y - Resources.Boxes["alien_up"].Y / 2,
            "up")
        {
        }

        void jump()
        {
            if (direction == "down") direction = "up";
            else direction = "down";
            Resources.Sounds["jump"].Position = TimeSpan.Zero;
            Resources.Sounds["jump"].Play();
        }

        public void tick()
        {
            if (direction == "up")
            {
                pos.Y -= 10;
                if (pos.Y <= 0) jump();
            }
            else
            {
                pos.Y += 10;
                if (pos.Y >= GameState.height - Resources.Boxes[RID].Y) jump();
            }
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
                pos.X -= 10;
                if (pos.X + Resources.Boxes[RID].X <= 0) flip();
            }
            else
            {
                pos.X += 10;
                if (pos.X >= GameState.width + Resources.Boxes[RID].X) flip();
            }
        }

    }

    public class GameState
    {
        public static readonly int width = 1000;
        public static readonly int height = 400;

        public List<Alien> aliens = new List<Alien>();
        internal List<GameObject> missiles = new List<GameObject>();

        public GameState()
        {
            aliens.Add(new Alien(width / 2, height / 2, "up"));
            missiles.Add(new Missile("fish", width, height / 2, "left"));
        }

        protected void checkCollision()
        {
        }

        public void tick()
        {
            foreach (Alien alien in aliens) alien.tick();
            foreach (Missile missile in missiles) missile.tick();
            checkCollision();
        }
    }
}
