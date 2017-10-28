using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Windows.Media;

namespace client
{
    public partial class VAUNCE : Form
    {
        Render render = new Render();
        GameState gameState = new GameState();
        HttpClient httpReq = new HttpClient();

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

            var diff = DateTime.Now - gameState.aliens[0].lastAlive;
            labelCurrentTime.Text = string.Format("Current Time\n{0}:{1:00}", diff.Seconds, diff.Milliseconds / 10);
            labelBestTime.Text = string.Format("Best Time\n{0}:{1:00}", gameState.bestTime.Seconds, gameState.bestTime.Milliseconds / 10);
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

        private void fetchStatus()
        {
            var values = new Dictionary<string, string> { };
            var content = new FormUrlEncodedContent(values);
            var response = httpReq.PostAsync("http://192.168.219.112:7788/vaunce/get_state", content).Result;
            if (!response.IsSuccessStatusCode) return;

            var cont = response.Content;
            var text = cont.ReadAsStringAsync().Result;

            gameState.ghosts.Clear();
            dynamic elems = Newtonsoft.Json.JsonConvert.DeserializeObject(text);
            foreach (var elem in elems.data.aliens)
            {
                var alien = new Alien((float)elem.pos[0], (float)elem.pos[1], (string)elem.direction);
                alien.name = elem.name;
                gameState.ghosts.Add(alien);
            }
            /*
            {"status": 200,
            "data": {"aliens": [{"direction": "up", "name": "peter", "best": 5, "pos": [52.9, 101.1]}, 
            {"direction": "down", "name": "nick", "best": 50, "pos": [521.9, 201.1]}], 
            "missiles": [], "best_score": ["nick", 50]}}
            */
            Console.Out.WriteLine(elems.data);
        }

        private void timerRender_Tick(object sender, EventArgs e)
        {
            fetchStatus();
            draw();
            gameState.tick();
        }

        private void VAUNCE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                var elem = gameState.aliens[0];
                elem.pos.X -= 17.7f;
                if (elem.pos.X <= 0) elem.pos.X = Resources.Boxes["bg"].X;
            }
            else if (e.KeyCode == Keys.Right)
            {
                var elem = gameState.aliens[0];
                elem.pos.X += 17.7f;
                if (elem.pos.X >= Resources.Boxes["bg"].X) elem.pos.X = 0;
            }
        }

        private void VAUNCE_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
    }
}
