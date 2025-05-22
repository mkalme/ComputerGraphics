using System;
using System.Drawing;

namespace Vectors
{
    public struct Vec3 {
        public float X, Y, Z;

        public float Length {
            get {
                return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            }
        }

        public Vec3(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        public void Normalize() {
            float l = Length;

            X /= l;
            Y /= l;
            Z /= l;
        }
        public Color ToColor() {
            int r = (int)(X * 255);
            int g = (int)(Y * 255);
            int b = (int)(Z * 255);

            r = r > 255 ? 255 : r;
            g = g > 255 ? 255 : g;
            b = b > 255 ? 255 : b;

            r = r < 0 ? 0 : r;
            g = g < 0 ? 0 : g;
            b = b < 0 ? 0 : b;

            return Color.FromArgb(r, g, b);
        }

        public static Vec3 operator +(Vec3 vec1, Vec3 vec2)
        {
            return new Vec3(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
        }
        public static Vec3 operator -(Vec3 vec1, Vec3 vec2)
        {
            return new Vec3(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
        }
        public static Vec3 operator *(Vec3 vec1, float d)
        {
            return new Vec3(vec1.X * d, vec1.Y * d, vec1.Z * d);
        }
        public static Vec3 operator *(float d, Vec3 vec1)
        {
            return new Vec3(vec1.X * d, vec1.Y * d, vec1.Z * d);
        }
        public static Vec3 operator *(Vec3 vec1, Vec3 vec2)
        {
            return new Vec3(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z);
        }
        public static Vec3 operator /(Vec3 vec1, float d)
        {
            return new Vec3(vec1.X / d, vec1.Y / d, vec1.Z / d);
        }
        public static Vec3 operator /(float d, Vec3 vec1)
        {
            return new Vec3(vec1.X / d, vec1.Y / d, vec1.Z / d);
        }

        public static Vec3 Cross(Vec3 vec1, Vec3 vec2) {
            float x = vec1.Y * vec2.Z - vec1.Z * vec2.Y;
            float y = vec1.Z * vec2.X - vec1.X * vec2.Z;
            float z = vec1.X * vec2.Y - vec1.Y * vec2.X;

            return new Vec3(x, y, z);
        }
        public static float Dot(Vec3 vec1, Vec3 vec2) {
            return vec1.X * vec2.X + vec1.Y * vec2.Y + vec1.Z * vec2.Z;
        }
        public static Vec3 Normalize(Vec3 vec)
        {
            float l = vec.Length;

            return new Vec3(vec.X / l, vec.Y / l, vec.Z / l);
        }
        public static float Distance(Vec3 p1, Vec3 p2) {
            float x = p2.X - p1.X;
            float y = p2.Y - p1.Y;
            float z = p2.Z - p1.Z;

            return (float)Math.Sqrt(x * x + y * y + z * z);
        }
        public static Vec3 Abs(Vec3 vec) {
            float x = vec.X < 0 ? -vec.X : vec.X;
            float y = vec.Y < 0 ? -vec.Y : vec.Y;
            float z = vec.Z < 0 ? -vec.Z : vec.Z;

            return new Vec3(x, y, z);
        }
        public static Vec3 Max(Vec3 vec, float v) {
            float x = vec.X > v ? vec.X : v;
            float y = vec.Y > v ? vec.Y : v;
            float z = vec.Z > v ? vec.Z : v;

            return new Vec3(x, y, z);
        }

        public static Vec3 SetLengthBetweenVec(Vec3 o, Vec3 d, float l) {
            float x = d.X - o.X;
            float y = d.Y - o.Y;
            float z = d.Z - o.Z;

            float di = (float)Math.Sqrt(x * x + y * y + z * z);
            float lr = l / di;

            return new Vec3(x * lr + o.X, y * lr + o.Y, z * lr + o.Z);
        }
    }
}
