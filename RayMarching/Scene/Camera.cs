using System;
using Vectors;
using Newtonsoft.Json.Linq;

namespace RayMarching {
    public class Camera {
        public int Width, Height;
        public float FocalLength;
        public Vec3 Pos;

        public Camera(int width, int height) {
            Width = width;
            Height = height;
            Pos = new Vec3(0, 0, 0);
        }

        internal Ray[,] GetRays() {
            Ray[,] rays = new Ray[Width, Height];

            float pxl = 1F / Width;
            
            float ppx = 0.5F - pxl / 2F;
            float ppy = Height / (float)Width / 2F - pxl / 2F;

            for (int yp = 0; yp < Height; yp++) {
                for (int xp = 0; xp < Width; xp++) {
                    float x = ppx - pxl * xp;
                    float y = ppy - pxl * yp;

                    rays[xp, yp] = new Ray(Pos, new Vec3(x, y, FocalLength) + Pos);
                }
            }

            return rays;
        }

        internal static Camera FromJToken(JToken token) {
            return token.ToObject<Camera>();
        }
    }
}
