﻿using System;
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
        Dictionary<string, MediaPlayer> _bgPlayers = new Dictionary<string, MediaPlayer>();
        Render render = new Render();

        public VAUNCE()
        {
            InitializeComponent();
            InitializeSounds();
        }

        private void draw()
        {
            var gContext = BufferedGraphicsManager.Current;
            var buffer = gContext.Allocate(renderArea.CreateGraphics(), renderArea.DisplayRectangle);

            buffer.Graphics.Clear(System.Drawing.Color.LightSteelBlue);
            render.render(buffer.Graphics, new GameState());

            buffer.Render(renderArea.CreateGraphics());
            buffer.Dispose();
        }

        private void InitializeSounds()
        {
            var keys = new List<string>() { "bg1", "bg2", "die", "jump" };
            foreach (var key in keys)
            {
                _bgPlayers.Add(key, new MediaPlayer());
                _bgPlayers[key].Open(new Uri(string.Format(@".\Sounds\{0}.wav", key), UriKind.Relative));
            }
            _bgPlayers["bg1"].MediaEnded += bg1MediaEnded;
        }

        private void btnMusic_Click(object sender, EventArgs e)
        {
            _bgPlayers["bg1"].Play();
            _bgPlayers["bg2"].Play();
        }

        private void bg1MediaEnded(object sender, EventArgs e)
        {
            _bgPlayers["bg1"].Position = TimeSpan.Zero;
            _bgPlayers["bg2"].Position = TimeSpan.Zero;
            _bgPlayers["bg1"].Play();
            _bgPlayers["bg2"].Play();
        }

        private void btnJump_Click(object sender, EventArgs e)
        {
            _bgPlayers["jump"].Position = TimeSpan.Zero;
            _bgPlayers["jump"].Play();
        }

        private void btnDie_Click(object sender, EventArgs e)
        {
            _bgPlayers["die"].Position = TimeSpan.Zero;
            _bgPlayers["die"].Play();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            draw();
        }
    }
}
