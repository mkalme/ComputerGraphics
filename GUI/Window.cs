using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Vectors;
using RayMarching;

namespace GUI {
    public partial class Window : Form {
        private static readonly bool AllowResize = false;

        private Image RenderedImage;

        public Window()
        {
            InitializeComponent();

            Render();
            ShowImage(RenderedImage);
        }

        private void Render() {
            string json = File.ReadAllText(@"D:\RayMarching\ThreeSphereReflectionSpecular.json");
            Scene scene = Scene.FromJSON(json);

            //Sphere sphere = new Sphere(new Vec3(0, -0.5F, 4), 0.5F);
            //sphere.Properties.Color = Color.Gold;
            //sphere.Properties.ReflectionIndex = 0.5F;
            //scene.Shapes.Add(sphere);

            //scene.Camera.Pos = new Vec3(0, -0.5F, 0);

            DateTime t1 = DateTime.Now;
            RenderedImage = new Renderer(scene).RenderScene();
            Console.WriteLine((DateTime.Now - t1).TotalSeconds + " seconds");
        }

        private void ShowImage(Image image) {
            Image output = image;
            if (AllowResize) output = ResizeImage(image, GraphicsPictureBox.Size);

            GraphicsPictureBox.Image = output;
        }
        private static Image ResizeImage(Image image, Size containerSize) {
            int width = containerSize.Width;
            int height = (int)(image.Height * (containerSize.Width / (float)image.Width));

            if (image.Height > containerSize.Width) {
                width = (int)(image.Width * (containerSize.Height / (float)image.Height));
                height = containerSize.Height;
            }

            if (width == 0 || height == 0) return image;

            Bitmap output = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(output)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(image, 0, 0, width, height);
            }

            return output;
        }
    }
}
