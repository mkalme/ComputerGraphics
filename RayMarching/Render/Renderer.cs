using System;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using Vectors;

namespace RayMarching {
    public class Renderer {
        private Scene Scene;

        private static readonly float MAX_DIST = 100;
        private static readonly float MIN_DIST = 0.001F;
        private static readonly int MAX_STEPS = 10000;
        private static readonly int MAX_DEPTH = 10;

        private static readonly float SPEC_POW = 100;

        public RenderProgress Progress;

        public Renderer(Scene scene) {
            Scene = scene;
        }

        public Bitmap RenderScene()
        {
            Progress = new RenderProgress(Scene.Camera.Width, Scene.Camera.Height);

            Progress.RenderStarted = DateTime.Now;
            Progress.Type = RenderProgressType.Running;
            OnRenderStart();

            Ray[,] rays = Scene.Camera.GetRays();
            Parallel.For(0, rays.GetLength(1), y => {
                for (int x = 0; x < rays.GetLength(0); x++) {
                    Progress._currentImage.SetPixel(x, y, CastRay(rays[x, y], 1));

                    Interlocked.Increment(ref Progress.PixelsRendered);
                }
            });

            //Ray ray = rays[576, 405];
            //CastRay(ray, 1);

            //Progress._currentImage.SetPixel(576, 405, Color.Red);

            Progress.RenderedImage = Progress._currentImage.Bitmap;

            Progress.Type = RenderProgressType.Finished;
            OnRenderFinished();

            return Progress.RenderedImage;
        }

        private Color CastRay(Ray ray, int depth) {
            MarchResults result = MarchRay(ray);
            if (!result.Intersects) return Color.Black;

            Shape shape = result.Shape;
            Vec3 point = result.Point;

            //Shading
            Shading shading = GetShadingColor(shape.GetNormal(point), point, ray, shape.GetColor(point));
            Color color = ColorExtensions.AddColors(shading.AmbientColor, shading.DiffusedColor);

            //Reflection
            if (shape.Properties.ReflectionIndex > 0 && depth < MAX_DEPTH) {
                Vec3 r = GetReflectionVector(result.Shape.GetNormal(point), ray);
                Ray rr = new Ray(point, point + r);

                color = CastRay(rr, depth + 1).AddTint(color, shape.Properties.ReflectionIndex);
            }

            return color.AddColor(shading.SpecuralColor);
        }
        private MarchResults MarchRay(Ray ray, float maxdist = 100) {
            MarchResults results = new MarchResults();

            float d = 0.03F;
            for (int i = 0; i < MAX_STEPS; i++) {
                Vec3 point = Vec3.SetLengthBetweenVec(ray.Origin, ray.Point2, d);

                Shape closestShape;
                float minD = Scene.GetDistanceToClosestShape(point, out closestShape);

                d += minD;

                if (minD <= MIN_DIST) {
                    return new MarchResults(true, closestShape, point, d);
                } else if (minD >= maxdist) {
                    return results;
                }
            }

            return results;
        }

        //Normal functions
        private static Vec3 GetReflectionVector(Vec3 n, Ray ray) {
            Vec3 d = ray.Point2 - ray.Origin;

            return d - (2 * Vec3.Dot(d, n) * n);
        }

        //Shading
        private Shading GetShadingColor(Vec3 n, Vec3 p, Ray vr, Color color)
        {
            Vec3 v = vr.Point2 - vr.Origin;
            v.Normalize();

            Vec3 obColor = color.ToVector();

            Vec3 amColor = new Vec3(0.05F, 0.05F, 0.05F) * obColor;
            Vec3 diColor = new Vec3(0, 0, 0);
            Vec3 spColor = new Vec3(0, 0, 0);

            foreach (LightSource light in Scene.LightSources) {
                float dist = Vec3.Distance(p, light.Pos);
                MarchResults shadowResults = MarchRay(new Ray(p, light.Pos), dist);
                if (shadowResults.Distance < dist) continue;

                //Console.WriteLine(p.X + ", " + p.Y + ", " + p.Z);

                Vec3 lv = light.Pos - p;
                lv.Normalize();

                Vec3 r = GetLightReflection(n, lv);
                r.Normalize();

                float li = light.GetIntensityFromPoint(p);

                float diffuse = Math.Max(Vec3.Dot(lv, n), 0) * 0.99F * li;

                float dots = Math.Max(Vec3.Dot(r, v), 0);
                float specular = (float)Math.Pow(dots, SPEC_POW);// * li;

                Vec3 liColor = light.Color.ToVector();

                diColor = diColor + (liColor * diffuse * obColor);
                spColor = spColor + (liColor * specular);
            }

            return new Shading(amColor.ToColor(), spColor.ToColor(), diColor.ToColor(), color);
        }
        private Shading GetShadingColorOld(Vec3 n, Vec3 p, Ray vr, Color color) {
            float specular = 0;
            float diffuse = 0;

            Vec3 v = vr.Point2 - vr.Origin;
            v.Normalize();

            foreach (LightSource light in Scene.LightSources) {
                MarchResults shadowResults = MarchRay(new Ray(p, light.Pos));
                if (shadowResults.Distance < Vec3.Distance(p, light.Pos)) continue;

                Vec3 lv = light.Pos - p;
                lv.Normalize();

                Vec3 r = GetLightReflection(n, lv);
                r.Normalize();

                float li = light.GetIntensityFromPoint(p);

                float dots = Math.Max(Vec3.Dot(r, v), 0);
                specular += (float)Math.Pow(dots, SPEC_POW);// * li;

                diffuse += Math.Max(Vec3.Dot(lv, n), 0) * 0.99F * li;
            }

            diffuse = diffuse.Clamp(0, 1);
            specular = specular.Clamp(0, 1);

            Color amColor = color.SetBrightness(0.0F);
            Color sColor = ColorExtensions.ColorFromBrightness(specular);
            Color difdColor = color.SetBrightness(diffuse);

            return new Shading(amColor, sColor, difdColor, color);
        }

        private static Vec3 GetLightReflection(Vec3 n, Vec3 l)
        {
            return l - (2 * Vec3.Dot(l, n) * n);
        }

        //Events
        public event EventHandler RenderStart;
        internal void OnRenderStart()
        {
            if (RenderStart != null) {
                RenderStart.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler RenderFinished;
        internal void OnRenderFinished()
        {
            if (RenderFinished != null) {
                RenderFinished.Invoke(this, EventArgs.Empty);
            }
        }
    }

    class Shading {
        public Color AmbientColor;
        public Color SpecuralColor;
        public Color DiffusedColor;
        public Color OriginalColor;

        public Shading(Color ambientColor, Color specularColor, Color diffusedColor, Color originalColor) {
            AmbientColor = ambientColor;
            SpecuralColor = specularColor;
            DiffusedColor = diffusedColor;
            OriginalColor = originalColor;
        }

        public Color GetShadingColor() {
            return ColorExtensions.AddColors(AmbientColor, SpecuralColor, DiffusedColor);
        }
    }

    //Code from https://stackoverflow.com/a/34801225/6597542
    public class DirectBitmap {
        public Bitmap Bitmap { get; private set; }
        private int[] Bits { get; set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new int[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, BitsHandle.AddrOfPinnedObject());
        }

        public static DirectBitmap FromPixelArray(Color[,] pixelArray) {
            DirectBitmap bitmap = new DirectBitmap(pixelArray.GetLength(0), pixelArray.GetLength(1));

            for (int y = 0; y < bitmap.Height; y++) {
                for (int x = 0; x < bitmap.Width; x++) {
                    bitmap.SetPixel(x, y, pixelArray[x, y]);
                }
            }

            return bitmap;
        }

        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + (y * Width);
            int col = colour.ToArgb();

            Bits[index] = col;
        }
    }
}