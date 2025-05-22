using System;
using Newtonsoft.Json.Linq;
using System.Drawing;
using Vectors;

namespace RayMarching {
    public class Rectangle : Shape {
        public Vec3 Pos2;

        public Rectangle(Vec2 p1, Vec2 p2, float w) : base(new Vec3(p1.X, w, p1.Y)) {
            Pos2 = new Vec3(p2.X, w, p2.Y);
        }

        internal override float GetDistance(Vec3 p)
        {
            Vec3 p1 = Pos, p2 = Pos2;
            if (Pos2.Z < Pos.Z) {
                p1 = Pos2; p2 = Pos;
            }

            if (p1.X > p2.X) {
                float x = p1.X;
                p1 = new Vec3(p2.X, p1.Y, p1.Z);
                p2 = new Vec3(x, p2.Y, p2.Z);
            }

            //Check corners
            if (p.X <= p1.X && p.Z <= p1.Z) {
                return Vec3.Distance(p, p1);
            } else if (p.X >= p2.X && p.Z <= p1.Z) {
                return Vec3.Distance(p, new Vec3(p2.X, p1.Y, p1.Z));
            } else if (p.X >= p2.X && p.Z >= p2.Z) {
                return Vec3.Distance(p, p2);
            } else if (p.X <= p1.X && p.Z >= p2.Z) {
                return Vec3.Distance(p, new Vec3(p1.X, p1.Y, p2.Z));
            }

            //Check sides
            if (p.X > p1.X && p.X < p2.X && p.Z <= p1.Z) {
                return Vec3.Distance(p, new Vec3(p.X, p1.Y, p1.Z));
            } else if (p.Z > p1.Z && p.Z < p2.Z && p.X >= p2.X) {
                return Vec3.Distance(p, new Vec3(p2.X, p1.Y, p.Z));
            } else if (p.X > p1.X && p.X < p2.X && p.Z >= p2.Z) {
                return Vec3.Distance(p, new Vec3(p.X, p1.Y, p2.Z));
            } else if (p.Z > p1.Z && p.Z < p2.Z && p.X <= p1.X) {
                return Vec3.Distance(p, new Vec3(p1.X, p1.Y, p.Z));
            }

            return Math.Abs(p.Y - p1.Y);
        }
        internal override Vec3 GetNormal(Vec3 point)
        {
            float y = point.Y - Pos.Y;

            if (y < 0) return new Vec3(0, -1, 0);
            return new Vec3(0, 1, 0);
        }

        private static readonly Color Color1 = Color.FromArgb(108, 115, 127);
        private static readonly Color Color2 = Color.FromArgb(153, 163, 184);
        internal override Color GetColor(Vec3 point)
        {

            int aq = (int)(point.X * 4);
            int bq = (int)(point.Z * 4);

            if (point.X < 0) aq++;
            if (point.Z < 0) bq++;

            if (aq % 2 == 0) {
                if (bq % 2 == 0) return Color1;
                else return Color2;
            } else {
                if (bq % 2 == 0) return Color2;
                else return Color1;
            }
        }

        internal static new Rectangle FromJToken(JToken token)
        {
            Vec2 pos1 = token["Pos1"].ToObject<Vec2>();
            Vec2 pos2 = token["Pos2"].ToObject<Vec2>();

            float w = (float)token["W"];

            Rectangle r = new Rectangle(pos1, pos2, w);
            r.Properties = ShapeProperties.FromJToken(token["Properties"]);

            return r;
        }
    }
}
