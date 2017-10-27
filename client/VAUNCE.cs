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
        MediaPlayer p1 = new MediaPlayer();
        MediaPlayer p2 = new MediaPlayer();

        public VAUNCE()
        {
            InitializeComponent();
            p1.MediaEnded += P1_MediaEnded;
        }

        private void btnMusic_Click(object sender, EventArgs e)
        {
            p2.Open(new Uri(@".\Sounds\bg2.wav", UriKind.Relative));
            p2.Play();
            p1.Open(new Uri(@".\Sounds\bg1.wav", UriKind.Relative));
            p1.Play();
        }

        private void P1_MediaEnded(object sender, EventArgs e)
        {
            p1.Position = TimeSpan.Zero;
            p1.Play();
            p2.Position = TimeSpan.Zero;
            p2.Play();
        }

        private void P2_MediaEnded(object sender, EventArgs e)
        {
        }
    }
}
