using System;
using System.Drawing;
using Vectors;
using Newtonsoft.Json.Linq;

namespace PathTracer {
    static class ColorExtensions {
        public static Color SetBrightness(this Color color, float brightness) {
            int r = (int)(color.R * brightness);
            int g = (int)(color.G * brightness);
            int b = (int)(color.B * brightness);

            return Color.FromArgb(r, g, b);
        }
        public static Color AddTint(this Color color, Color shade, float intensity)
        {
            intensity = 1 - intensity;

            int r = color.R;
            int g = color.G;
            int b = color.B;

            r = r + (int)((shade.R - r) * intensity);
            g = g + (int)((shade.G - g) * intensity);
            b = b + (int)((shade.B - b) * intensity);

            return Color.FromArgb(r, g, b);
        }
        public static Color AddColor(this Color color, Color value)
        {
            int r = color.R + value.R;
            int g = color.G + value.G;
            int b = color.B + value.B;

            r = r > 255 ? 255 : r;
            g = g > 255 ? 255 : g;
            b = b > 255 ? 255 : b;

            return Color.FromArgb(r, g, b);
        }
        public static Color ToGamma(this Color color, double gamma)
        {
            int r = (int)(Math.Pow(color.R / 255.0, 1 / gamma) * 255);
            int g = (int)(Math.Pow(color.G / 255.0, 1 / gamma) * 255);
            int b = (int)(Math.Pow(color.B / 255.0, 1 / gamma) * 255);

            return Color.FromArgb(r, g, b);
        }
        public static Vec3 ToVector(this Color color)
        {
            float r = color.R / 255.0F;
            float g = color.G / 255.0F;
            float b = color.B / 255.0F;

            return new Vec3(r, g, b);
        }

        public static Color ColorFromJToken(JToken token) {
            if (token.Type == JTokenType.Null) return Color.Black;

            int r = (int)token["R"];
            int g = (int)token["G"];
            int b = (int)token["B"];

            return Color.FromArgb(r, g, b);
        }
        public static Color ColorFromBrightness(float brightness) {
            int w = (int)(255 * brightness);

            return Color.FromArgb(w, w, w);
        }
        public static Color AddColors(params Color[] colors) {
            int r = 0, g = 0, b = 0;

            foreach (Color color in colors) {
                r += color.R;
                g += color.G;
                b += color.B;
            }

            r = r > 255 ? 255 : r;
            g = g > 255 ? 255 : g;
            b = b > 255 ? 255 : b;

            r = r < 0 ? 0 : r;
            g = g < 0 ? 0 : g;
            b = b < 0 ? 0 : b;

            return Color.FromArgb(r, g, b);
        }
        public static Color ColorFromVector(Vec3 vec) {
            int r = (int)(vec.X * 255);
            int g = (int)(vec.Y * 255);
            int b = (int)(vec.Z * 255);

            r = r > 255 ? 255 : r;
            g = g > 255 ? 255 : g;
            b = b > 255 ? 255 : b;

            return Color.FromArgb(r, g, b);
        }
    }

    static class FloatExtensions {
        public static float Clamp(this float v, float p1, float p2) {
            v = v < p1 ? p1 : v;
            v = v > p2 ? p2 : v;

            return v;
        }
    }
}
