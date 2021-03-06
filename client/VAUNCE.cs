﻿using Newtonsoft.Json;
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
        string myName = "";
        int bestScore = 0;
        string bestName = "";

        public VAUNCE()
        {
            InitializeComponent();

            myName = Microsoft.VisualBasic.Interaction.InputBox("name?", "give me your name", "");

            Resources.Sounds["bg1"].MediaEnded += bg1MediaEnded;
            timerRender.Start();
            timerTCP.Start();
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
            labelCurrentTime.Text = string.Format("Current Time\n{0}:{1:00}", diff.Minutes * 60 + diff.Seconds, diff.Milliseconds / 10);
            labelBestTime.Text = string.Format("Best Time\n{0}:{1:00}", gameState.bestTime.Minutes * 60 + gameState.bestTime.Seconds, gameState.bestTime.Milliseconds / 10);
            labelBestUser.Text = string.Format("Best User: {0}\n{1}:{2:00}", bestName, bestScore / 1000, (bestScore % 1000) / 10);
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
            HttpClient httpReq = new HttpClient();
            var values = new Dictionary<string, string> { };
            var content = new FormUrlEncodedContent(values);
            var response = httpReq.PostAsync("http://192.168.219.111:7788/vaunce/get_state", content).Result;
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
                var best = (int)elem.best;
                if (bestScore <= best)
                {
                    bestScore = best;
                    bestName = (string)elem.name;
                }
            }
            /*
            {"status": 200,
            "data": {"aliens": [{"direction": "up", "name": "peter", "best": 5, "pos": [52.9, 101.1]}, 
            {"direction": "down", "name": "nick", "best": 50, "pos": [521.9, 201.1]}], 
            "missiles": [], "best_score": ["nick", 50]}}
            */
            // Console.Out.WriteLine(elems.data);
        }

        private void sendStatus()
        {
            // {"direction": "up", "name": "peter", "best": 5}

            var alien = gameState.aliens[0];
            dynamic payload = new System.Dynamic.ExpandoObject();
            payload.name = alien.name;
            payload.direction = alien.direction;
            payload.pos = (new List<float>() { alien.pos.X, alien.pos.Y }).ToArray();
            payload.best = gameState.bestTime.Minutes * 60 * 1000 + gameState.bestTime.Seconds * 1000 + gameState.bestTime.Milliseconds;
            var body = JsonConvert.SerializeObject(payload);
            //Console.Out.WriteLine(body);

            HttpClient httpReq = new HttpClient();
            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            var response = httpReq.PostAsync("http://192.168.219.111:7788/vaunce/update_alien", httpContent).Result;
        }

        private void timerRender_Tick(object sender, EventArgs e)
        {
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
            var alien = new Alien(Resources.Boxes["bg"].X / 2, Resources.Boxes["bg"].Y / 2, "up");
            alien.name = myName;
            gameState.aliens.Add(alien);
            gameState.missiles.Add(new Missile("fish", Resources.Boxes["bg"].X, Resources.Boxes["bg"].Y / 2, "left"));
        }

        private void timerTCP_Tick(object sender, EventArgs e)
        {
            sendStatus();
            fetchStatus();
        }
    }
}
