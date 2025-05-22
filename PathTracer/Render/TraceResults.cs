using System;
using Vectors;

namespace PathTracer {
    class TraceResults {
        public bool Intersects = false;
        public Shape Shape = null;
        public Vec3 Point;
        public float Distance = float.MaxValue;

        public TraceResults() {

        }
        public TraceResults(bool intersects, Shape shape, Vec3 point, float distance) {
            Intersects = intersects;
            Shape = shape;
            Point = point;
            Distance = distance;
        }
    }
}
