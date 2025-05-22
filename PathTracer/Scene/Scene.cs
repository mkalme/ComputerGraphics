using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Vectors;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PathTracer 
{
    public class Scene
    {
        public Camera Camera;
        public List<Shape> Shapes;
        public List<LightSource> LightSources;

        public Scene(){
            Shapes = new List<Shape>();
            LightSources = new List<LightSource>();
        }

        internal float GetDistanceToClosestShape(Vec3 point, out Shape closestShape){
            if (Shapes.Count == 0) {
                closestShape = null;
                return 100;
            }

            float[] ds = new float[Shapes.Count];

            for (int i = 0; i < Shapes.Count; i++) {
                ds[i] = Shapes[i].GetDistance(point);
            }

            int index = Array.IndexOf(ds, ds.Min());

            closestShape = Shapes[index];

            return ds[index];
        }

        public static Scene FromJSON(string json){
            JObject obj = JsonConvert.DeserializeObject<JObject>(json);

            Camera camera = Camera.FromJToken(obj["Camera"]);

            JArray lightsObj = (JArray)obj["LightSources"];
            List<LightSource> lightSources = new List<LightSource>();
            foreach (JToken token in lightsObj) {
                lightSources.Add(LightSource.FromJToken(token));
            }

            JArray shapesObj = (JArray)obj["Shapes"];
            List<Shape> shapes = new List<Shape>();
            foreach (JToken token in shapesObj) {
                shapes.Add(Shape.FromJToken(token));
            }

            Scene scene = new Scene() {
                Camera = camera,
                LightSources = lightSources,
                Shapes = shapes
            };

            return scene;
        }
    }
}
