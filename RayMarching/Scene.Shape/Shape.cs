using System;
using System.Drawing;
using Vectors;
using Newtonsoft.Json.Linq;

namespace RayMarching {
    public class Shape {
        public Vec3 Pos;
        public ShapeProperties Properties;

        public Shape(Vec3 pos) {
            Pos = pos;
            Properties = new ShapeProperties(Color.White);
        }
        public Shape(Vec3 pos, Color color) {
            Pos = pos;
            Properties = new ShapeProperties(color);
        }

        internal virtual float GetDistance(Vec3 point) {
            return 0;
        }
        internal virtual Vec3 GetNormal(Vec3 point) {
            return new Vec3();
        }

        internal virtual Color GetColor(Vec3 p) {
            return Properties.Color;
        }

        internal static Shape FromJToken(JToken token) {
            string type = token["ShapeType"].ToString();

            switch (type) {
                case "Plane":
                    return Plane.FromJToken(token);
                case "Sphere":
                    return Sphere.FromJToken(token);
                case "Capsule":
                    return Capsule.FromJToken(token);
                case "Torus":
                    return Torus.FromJToken(token);
                case "Rectangle":
                    return Rectangle.FromJToken(token);
            }

            Shape shape = new Shape(token["Pos"].ToObject<Vec3>());
            shape.Properties = ShapeProperties.FromJToken(token["Properties"]);

            return shape;
        }
    }
}
