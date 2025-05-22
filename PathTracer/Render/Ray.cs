using System;
using Vectors;

namespace PathTracer {
    class Ray {
        public Vec3 Origin;
        public Vec3 Point2;

        public Ray(Vec3 origin, Vec3 point2)
        {
            Origin = origin;
            Point2 = point2;
        }
        public Ray(float x, float y, float z)
        {
            Origin = new Vec3(0, 0, 0);
            Point2 = new Vec3(x, y, z);
        }
    }
}
