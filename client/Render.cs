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
        public Render()
        {
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
            g.DrawImage(Resources.Images["bg"], 0, 0);
        }

        protected void renderDynamicBackground(Graphics g, GameState state)
        {
        }

        private void renderMissiles(Graphics g, GameState state)
        {
            foreach (var missile in state.missiles) renderGameObject(g, missile);
        }

        protected void renderGameObject(Graphics g, GameObject obj)
        {
            g.DrawImage(Resources.Images[obj.RID], obj.pos.X, obj.pos.Y);
        }

        protected void renderAliens(Graphics g, GameState state)
        {
            foreach (var alien in state.aliens) renderGameObject(g, alien);
        }
    }
}
