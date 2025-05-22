using System;
using Vectors;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace RayMarching {
    public class Capsule : Shape {
        public Vec3 Pos2;
        public float Radius;

        public Capsule(Vec3 p1, Vec3 p2, float radius) : base(p1) {
            Pos2 = p2;
            Radius = radius;
        }

        internal override float GetDistance(Vec3 p)
        {
            Vec3 ab = Pos2 - Pos;
            Vec3 ap = p - Pos;

            float t = Vec3.Dot(ab, ap) / Vec3.Dot(ab, ab);
            t = t > 1 ? 1 : t;
            t = t < 0 ? 0 : t;

            Vec3 c = Pos + (t * ab);
            return (p - c).Length - Radius;
        }

        internal override Vec3 GetNormal(Vec3 p)
        {
            Vec3 ab = Pos2 - Pos;
            Vec3 ap = p - Pos;

            float t = Vec3.Dot(ab, ap) / Vec3.Dot(ab, ab);

            if (t < 0) {
                return Vec3.Normalize(p - Pos);
            } else if (t > 1) {
                return Vec3.Normalize(p - Pos2);
            } else { 
                Vec3 c = Pos + (t * ab);

                return Vec3.Normalize(p - c);
            }
        }

        internal static new Capsule FromJToken(JToken token)
        {
            Vec3 pos1 = token["Pos1"].ToObject<Vec3>();
            Vec3 pos2 = token["Pos2"].ToObject<Vec3>();
            float radius = (float)token["Radius"];

            Capsule capsule = new Capsule(pos1, pos2, radius);
            capsule.Properties = ShapeProperties.FromJToken(token["Properties"]);

            return capsule;
        }
    }
}
