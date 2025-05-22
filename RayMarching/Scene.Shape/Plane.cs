using System;
using System.Drawing;
using Vectors;
using Newtonsoft.Json.Linq;

namespace RayMarching {
    public class Plane : Shape {
        public PlaneType Type;

        public Plane(float w, PlaneType type) : base(PlaneHelper.GetPlaneVector(w, type)) {
            Type = type;
        }

        internal override Color GetColor(Vec3 point) {
            float a, b;
            SetTypeCoordinates(point, out a, out b);

            int aq = (int)(a * 1F);
            int bq = (int)(b * 1F);

            if (a < 0) aq++;
            if (b < 0) bq++;

            if (aq % 2 == 0) {
                if (bq % 2 == 0) return Color.DarkGray;
                else return Color.Gray;
            } else {
                if (bq % 2 == 0) return Color.Gray;
                else return Color.DarkGray;
            }
        }
        private void SetTypeCoordinates(Vec3 point, out float a, out float b) {
            if (Type == PlaneType.X) {
                a = point.Z; b = point.Y;
                return;
            } else if (Type == PlaneType.Z) {
                a = point.X; b = point.Y;
                return;
            }

            a = point.X; b = point.Z;
        }

        internal override float GetDistance(Vec3 vec) {
            if (Type == PlaneType.X) 
                return Math.Abs(vec.X - Pos.X);
            else if (Type == PlaneType.Z) 
                return Math.Abs(vec.Z - Pos.Z);

            return Math.Abs(vec.Y - Pos.Y);
        }
        internal override Vec3 GetNormal(Vec3 point)
        {
            if (Type == PlaneType.X) 
                return new Vec3(SwitchNormal(point.X - Pos.X), 0, 0);
            else if (Type == PlaneType.Y) 
                return new Vec3(0, SwitchNormal(point.Y - Pos.Y), 0);
            else
                return new Vec3(0, 0, SwitchNormal(point.Z - Pos.Z));
        }
        private static float SwitchNormal(float d) {
            if (d <= 0) return -1;
            return 1;
        }

        internal static new Plane FromJToken(JToken token) {
            float w = (float)token["W"];

            PlaneType planeType = PlaneType.Z;

            string type = token["PlaneType"].ToString();
            switch (type) {
                case "X":
                    planeType = PlaneType.X;
                    break;
                case "Y":
                    planeType = PlaneType.Y;
                    break;
            }

            Plane plane = new Plane(w, planeType);
            plane.Properties = ShapeProperties.FromJToken(token["Properties"]);

            return plane;
        }
    }

    public enum PlaneType {
        X,
        Y,
        Z,
    }
}
