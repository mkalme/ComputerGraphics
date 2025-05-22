using System;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace PathTracer {
    public class ShapeProperties {
        public Color Color;
        public float ReflectionIndex;
        public float RefractionIndex;
        public float DiffusionIndex;

        public ShapeProperties(Color color)
        {
            Color = color;
            ReflectionIndex = 0;
            RefractionIndex = 0;
            DiffusionIndex = 0;
        }
        public ShapeProperties(Color color, float reflection, float refraction, float diffusion)
        {
            Color = color;
            ReflectionIndex = reflection;
            RefractionIndex = refraction;
            DiffusionIndex = diffusion;
        }

        internal static ShapeProperties FromJToken(JToken token)
        {
            Color color = ColorExtensions.ColorFromJToken(token["Color"]);
            float reflection = (float)token["ReflectionIndex"];
            float refraction = (float)token["RefractionIndex"];
            float diffusion = (float)token["DiffusionIndex"];

            return new ShapeProperties(color, reflection, refraction, diffusion);
        }
    }
}
