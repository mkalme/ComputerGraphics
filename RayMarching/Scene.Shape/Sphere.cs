using System;
using Vectors;
using Newtonsoft.Json.Linq;

namespace RayMarching {
    public class Sphere : Shape {
        public float Radius;

        public Sphere(Vec3 pos, float radius) : base(pos) {
            Radius = radius;
        }

        internal override float GetDistance(Vec3 point) {
            return Vec3.Distance(Pos, point) - Radius;
        }
        internal override Vec3 GetNormal(Vec3 point)
        {
            Vec3 n = point - Pos;

            return new Vec3(n.X / Radius, n.Y / Radius, n.Z / Radius);
        }

        internal static new Sphere FromJToken(JToken token) {
            Vec3 pos = token["Pos"].ToObject<Vec3>();
            float radius = (float)token["Radius"];

            Sphere sphere = new Sphere(pos, radius);
            sphere.Properties = ShapeProperties.FromJToken(token["Properties"]);

            return sphere;
        }
    }
}
