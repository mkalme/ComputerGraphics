using System;
using System.ComponentModel;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PathTracer;
using Vectors;

namespace GUI {
    public partial class RenderDialog : Form {
        private BackgroundWorker BackgroundWorker = new BackgroundWorker();

        public Renderer Renderer;

        private ProgressPanel ProgressPanel;

        public RenderDialog() : this(new Renderer(GetTestScene())) {

        }
        public RenderDialog(Renderer renderer)
        {
            InitializeComponent();

            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.WorkerSupportsCancellation = true;
            BackgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);

            Renderer = renderer;
            Renderer.RenderFinished += new EventHandler(Renderer_Finished);

            ProgressPanel = new ProgressPanel(CreateGraphics());
            ProgressPanelContainer.Controls.Add(ProgressPanel);
        }

        private static Scene GetTestScene() {
            Scene scene = new Scene();
            scene.Camera = new Camera(850, 750);
            scene.Camera.FocalLength = 1;

            scene.LightSources.Add(new LightSource(new Vec3(-1, 1, 0), 10000));

            Sphere sphere = new Sphere(new Vec3(0, 0, 6), 1);
            sphere.Properties.ReflectionIndex = 0.8F;

            //scene.Shapes.Add(sphere);
            scene.Shapes.Add(new Sphere(new Vec3(-0.7F, 0, 4), 0.5F));

            //string json = File.ReadAllText(@"D:\RayMarching\ThreeSphereReflectionSpecular.json");
            //Scene scene = Scene.FromJSON(json);

            //scene.LightSources.RemoveAt(1);
            //scene.Shapes.Clear();

            //Sphere sphere = new Sphere(new Vec3(0, -0.5F, 4), 0.5F);
            //sphere.Properties.Color = Color.Gold;
            //sphere.Properties.ReflectionIndex = 0;
            //scene.Shapes.Add(sphere);

            //scene.Camera.Pos = new Vec3(0, -0.5F, 0);

            //scene.Camera.Width *= 2;
            //scene.Camera.Height *= 2;

            return scene;
        }

        private void RenderDialog_Load(object sender, EventArgs e)
        {
            StartRender();
        }

        private void StartRender() {
            DateTime t1 = DateTime.Now;
            Thread thread = new Thread(() => {
                Renderer.RenderScene();

                Console.WriteLine((DateTime.Now - t1).TotalSeconds + " seconds");
            });
            thread.Start();

            BackgroundWorker.RunWorkerAsync();
        }
        private void StopRender() {
            BackgroundWorker.CancelAsync();

            UpdateImage(Renderer.Progress.RenderedImage);
            UpdatePanel();

            Clipboard.SetImage(Renderer.Progress.RenderedImage);
        }

        private void UpdateImage(Image image){
            GraphicsPictureBox.Image = image;
        }
        private void UpdatePanel() {
            ProgressPanel.UpdatePanel(Renderer.Progress);
        }

        //BackgroundWorker
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            Thread.Sleep(200);

            while (!BackgroundWorker.CancellationPending) {
                BackgroundWorker.ReportProgress(Renderer.Progress.PixelsRendered);

                Thread.Sleep(50);
            }
        }
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            UpdateImage(Renderer.Progress.CurrentImage);

            if ((DateTime.Now - _lastUpdated).TotalMilliseconds < 100) return;
            _lastUpdated = DateTime.Now;
            UpdatePanel();
        }
        private DateTime _lastUpdated = DateTime.MinValue;

        //Renderer Events
        private void Renderer_Finished(object sender, EventArgs e)
        {
            Invoke(new Action(() => StopRender()));
        }

        //Resize PictureBox
        private void Container_SizeChanged(object sender, EventArgs e)
        {
            ResizePictureBox();
        }
        private void ResizePictureBox() {
            GraphicsPictureBox.Width = Math.Max(PictureBoxContainer.Width, GraphicsPictureBox.Image.Width);
            GraphicsPictureBox.Height = Math.Max(PictureBoxContainer.Height, GraphicsPictureBox.Image.Height);
        }
    }
}
