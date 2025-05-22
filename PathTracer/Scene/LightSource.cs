using System;
using System.Drawing;
using Vectors;
using Newtonsoft.Json.Linq;

namespace PathTracer {
    public class LightSource {
        internal static readonly float MIN_LUMENS = 600;
        public Vec3 Pos;
        public float Lumens;
        public Color Color;

        public LightSource(Vec3 pos, float lumens)
        {
            Pos = pos;
            Lumens = lumens;
            Color = Color.White;
        }
        public LightSource(float x, float y, float z, float lumens)
        {
            Pos = new Vec3(x, y, z);
            Lumens = lumens;
            Color = Color.White;
        }

        public float GetIntensityFromPoint(Vec3 point)
        {
            float d = Vec3.Distance(Pos, point);
            float l = Lumens / (d * d);

            float i = l / MIN_LUMENS;
            i = i > 1 ? 1 : i;

            return i;
        }

        internal static LightSource FromJToken(JToken token)
        {
            return new LightSource(token["Pos"].ToObject<Vec3>(), (float)token["Lumens"]);
        }
    }
}
