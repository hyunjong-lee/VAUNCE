using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace client
{
    public partial class VAUNCE : Form
    {
        Render render = new Render();
        GameState gameState = new GameState();

        public VAUNCE()
        {
            InitializeComponent();

            Resources.Sounds["bg1"].MediaEnded += bg1MediaEnded;
            timerRender.Start();
            playBGM();
        }

        private void draw()
        {
            var gContext = BufferedGraphicsManager.Current;
            var buffer = gContext.Allocate(renderArea.CreateGraphics(), renderArea.DisplayRectangle);

            buffer.Graphics.Clear(System.Drawing.Color.LightSteelBlue);
            render.render(buffer.Graphics, gameState);

            buffer.Render(renderArea.CreateGraphics());
            buffer.Dispose();
        }

        private void playBGM()
        {
            Resources.Sounds["bg1"].Position = TimeSpan.Zero;
            Resources.Sounds["bg2"].Position = TimeSpan.Zero;
            Resources.Sounds["bg1"].Play();
            Resources.Sounds["bg2"].Play();
        }

        private void btnMusic_Click(object sender, EventArgs e)
        {
            Resources.Sounds["bg1"].Play();
            Resources.Sounds["bg2"].Play();
        }

        private void bg1MediaEnded(object sender, EventArgs e)
        {
            playBGM();
        }

        private void btnDie_Click(object sender, EventArgs e)
        {
            Resources.Sounds["die"].Position = TimeSpan.Zero;
            Resources.Sounds["die"].Play();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            draw();
        }

        private void timerRender_Tick(object sender, EventArgs e)
        {
            draw();
            gameState.tick();
        }
    }
}
