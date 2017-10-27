using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace client
{
    public static class Resources
    {
        public static Dictionary<string, MediaPlayer> Sounds = new Dictionary<string, MediaPlayer>();
        public static Dictionary<string, Image> Images = new Dictionary<string, Image>();
        public static Dictionary<string, Point> Boxes = new Dictionary<string, Point>();

        static Resources()
        {
            InitializeSounds();
            InitializeImages();
        }

        private static void InitializeSounds()
        {
            var keys = new List<string>() { "bg1", "bg2", "die", "jump" };
            foreach (var key in keys)
            {
                Sounds.Add(key, new MediaPlayer());
                Sounds[key].Open(new Uri(string.Format(@".\Sounds\{0}.wav", key), UriKind.Relative));
            }
        }

        private static void InitializeImages()
        {
            var keys = new List<string>() { "bg", "fish_left", "fish_right", "slime_left", "snail_left", "alien_up", "alien_down" };
            foreach (var key in keys)
            {
                var image = Image.FromFile(string.Format(@"Images/{0}.png", key));
                Images.Add(key, image);
                Boxes.Add(key, new Point(image.Width, image.Height));
            }
            Boxes["bg"] = new Point(1000, 400);
        }
    }
}
