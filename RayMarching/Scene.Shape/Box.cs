using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vectors;

namespace RayMarching {
    public class Box : Shape {
        public Vec3 Size;

        public Box(Vec3 pos, Vec3 size) : base(pos) {
            Size = size;
        }

        internal override float GetDistance(Vec3 p)
        {
            return Vec3.Max(Vec3.Abs(p - Pos) - (Size / 2), 0).Length;
        }
        internal override Vec3 GetNormal(Vec3 point)
        {
            return new Vec3(0, 1, 0);
        }
    }
}
