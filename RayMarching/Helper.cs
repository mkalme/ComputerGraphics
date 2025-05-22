using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vectors;

namespace RayMarching {
    static class PlaneHelper {
        public static Vec3 GetPlaneVector(float w, PlaneType type)
        {
            if (type == PlaneType.X)
                return new Vec3(w, 0, 0);
            else if (type == PlaneType.Z)
                return new Vec3(0, 0, w);

            return new Vec3(0, w, 0);
        }
    }
}
