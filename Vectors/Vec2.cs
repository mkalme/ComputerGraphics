using System;

namespace Vectors {
    public struct Vec2 {
        public float X, Y;

        public float Length {
            get {
                float l = (float)Math.Sqrt(X * X + Y * Y);

                return l;
            }
        }

        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Normalize()
        {
            float l = Length;

            X /= l;
            Y /= l;
        }
        public void AddLength(float l)
        {
            float lr = l / Length + 1;

            X *= lr;
            Y *= lr;
        }

        public static Vec2 operator +(Vec2 vec1, Vec2 vec2)
        {
            return new Vec2(vec1.X + vec2.X, vec1.Y + vec2.Y);
        }
        public static Vec2 operator -(Vec2 vec1, Vec2 vec2)
        {
            return new Vec2(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }
        public static Vec2 operator *(Vec2 vec1, float d)
        {
            return new Vec2(vec1.X * d, vec1.Y * d);
        }
        public static Vec2 operator *(float d, Vec2 vec1)
        {
            return new Vec2(vec1.X * d, vec1.Y * d);
        }
        public static Vec2 operator /(Vec2 vec1, float d)
        {
            return new Vec2(vec1.X / d, vec1.Y / d);
        }
        public static Vec2 operator /(float d, Vec2 vec1)
        {
            return new Vec2(vec1.X / d, vec1.Y / d);
        }

        public static float Dot(Vec2 vec1, Vec2 vec2)
        {
            return vec1.X * vec2.X + vec1.Y * vec2.Y;
        }
        public static float GetLength(Vec2 vec) {
            return (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
        }
        public static Vec2 Normalize(Vec2 vec)
        {
            float l = vec.Length;

            return new Vec2(vec.X / l, vec.Y / l);
        }
        public static float Distance(Vec2 p1, Vec2 p2)
        {
            Vec2 n = p2 - p1;

            float l = (float)Math.Sqrt(n.X * n.X + n.Y * n.Y);

            return l;
        }
        public static Vec2 AddLength(Vec2 vec, float l)
        {
            float lr = l / vec.Length + 1;

            return new Vec2(vec.X *= lr, vec.Y *= lr);
        }
    }
}
