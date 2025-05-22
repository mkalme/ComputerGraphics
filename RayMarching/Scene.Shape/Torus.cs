using System;
using System.Drawing;
using Vectors;
using Newtonsoft.Json.Linq;

namespace RayMarching {
    public class Torus : Shape {
        public float Radius1;
        public float Radius2;

        public Torus(Vec3 pos, float r1, float r2) : base(pos) {
            Radius1 = r1;
            Radius2 = r2;
        }

        internal override float GetDistance(Vec3 p)
        {
            float x =  Vec3.Distance(new Vec3(p.X, Pos.Y, p.Z), Pos) - Radius1;

            return new Vec2(x, p.Y - Pos.Y).Length - Radius2;
        }

        internal override Vec3 GetNormal(Vec3 point)
        {
            Vec3 p2 = new Vec3(point.X, Pos.Y, point.Z);

            Vec3 p = Vec3.SetLengthBetweenVec(Pos, p2, Radius1);
            Vec3 n = point - p;
            n.Normalize();

            return n;
        }

        internal static new Torus FromJToken(JToken token)
        {
            Vec3 pos = token["Pos"].ToObject<Vec3>();
            
            float r1 = (float)token["Radius1"];
            float r2 = (float)token["Radius2"];

            Torus torus = new Torus(pos, r1, r2);
            torus.Properties = ShapeProperties.FromJToken(token["Properties"]);

            return torus;
        }
    }
}
