using System;
using System.Drawing;

namespace PathTracer {
    public class RenderProgress {
        public int Width { get; set; }
        public int Height { get; set; }
        public int PixelsRendered;

        internal DirectBitmap _currentImage;

        public Bitmap CurrentImage {
            get {
                return _currentImage.Bitmap;
            }
        }
        public Bitmap RenderedImage { get; set; }

        public RenderProgressType Type { get; set; }
        public DateTime RenderStarted { get; set; }

        public RenderProgress(int width, int height) {
            Width = width;
            Height = height;
            PixelsRendered = 0;
            Type = RenderProgressType.NotStarted;
            RenderStarted = DateTime.MinValue;

            _currentImage = new DirectBitmap(width, height);
        }

        private string[] DisplayProgress() {
            string[] p = new string[5];

            p[0] = string.Format("Started: {0}", RenderStarted.ToString());
            p[1] = string.Format("Width: {0}", Width);
            p[2] = string.Format("Height: {0}", Height);
            string percent = string.Format("{0:0.00}", (float)PixelsRendered / (Width * Height) * 100);
            p[3] = string.Format("Pixels Rendered: {0} \\ {1} ({2}%)", PixelsRendered, Width * Height, percent);
            string seconds = string.Format("{0:0.00}", (DateTime.Now - RenderStarted).TotalSeconds);
            p[4] = string.Format("Progress Type: {0} ({1} seconds)", Type, seconds);

            return p;
        }
    }

    public enum RenderProgressType { 
        NotStarted,
        Running,
        Finished
    }
}
