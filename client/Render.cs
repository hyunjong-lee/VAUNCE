using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class Render
    {
        private Dictionary<string, Image> _imageResources;

        public Render()
        {
            _imageResources = new Dictionary<string, Image>();
            var keys = new List<string>() { "bg", "fish", "slime", "snail" };
            foreach (var key in keys)
            {
                _imageResources.Add(key, Image.FromFile(string.Format(@"Images/{0}.png", key)));
            }
        }

        public void render(Graphics g, GameState state)
        {
            renderStaticBackground(g, state);
            renderDynamicBackground(g, state);
            renderMissiles(g, state);
            renderAliens(g, state);
        }

        protected void renderStaticBackground(Graphics g, GameState state)
        {
            g.DrawImage(_imageResources["bg"], 0, 0);
        }

        protected void renderDynamicBackground(Graphics g, GameState state)
        {
        }

        private void renderMissiles(Graphics g, GameState state)
        {
            g.DrawImage(_imageResources["fish"], 50, 100);
            g.DrawImage(_imageResources["slime"], 200, 200);
            g.DrawImage(_imageResources["snail"], 800, 200);
        }

        protected void renderAliens(Graphics g, GameState state)
        {
        }
    }
}
