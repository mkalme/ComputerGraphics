using System;
using Vectors;
using Newtonsoft.Json.Linq;

namespace PathTracer {
    public class Sphere : Shape {
        public float Radius;

        public Sphere(Vec3 pos, float radius) : base(pos) {
            Radius = radius;
        }

        internal override float GetDistance(Vec3 point)
        {
            return Vec3.Distance(Pos, point) - Radius;
        }
        internal override TraceResults Intersect(Ray ray)
        {
            Vec3 oc = ray.Origin - Pos;
            float ocl = oc.Length;

            float l =  (ray.Point2 - ray.Origin).Length;

            float a = l * l;
            float b = Vec3.Dot(oc, ray.Point2);
            float c = (ocl * ocl) - Radius * Radius;

            float disc = b * b - a * c;
            if (disc < 0) return new TraceResults();

            disc = (float)Math.Sqrt(disc);

            float t0 = -b - disc;
            float t1 = -b + disc;

            if (t1 < 0) return new TraceResults();

            float t = t0 >= 0 ? t0 : t1;

            return new TraceResults(true, this, ray.Origin + (ray.Point2 * t), t);
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
