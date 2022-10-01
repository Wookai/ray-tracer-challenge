namespace RayTracerChallenge
{
    public readonly struct Tensor
    {
        public readonly float x;
        public readonly float y;
        public readonly float z;
        public readonly float w;

        private const float POINT_W = 1.0f;
        private const float VECTOR_W = 0.0f;

        public Tensor(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public readonly bool IsPoint() => w == POINT_W;

        public readonly bool IsVector() => !IsPoint();

        // override object.Equals
        public override readonly bool Equals(object? obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Tensor other = (Tensor)obj;
            return x == other.x && y == other.y && z == other.z && w == other.w;
        }

        public static bool operator ==(Tensor left, Tensor right) => left.Equals(right);

        public static bool operator !=(Tensor left, Tensor right) => !(left == right);

        // override object.GetHashCode
        public override readonly int GetHashCode() => Tuple.Create(x, y, z, w).GetHashCode();

        public override readonly string ToString() => String.Format("{3}({0}, {1}, {2})", x, y, z, IsPoint() ? "Point" : "Vector");

        public static Tensor Point(float x, float y, float z) => new Tensor(x, y, z, POINT_W);

        public static Tensor Vector(float x, float y, float z) => new Tensor(x, y, z, VECTOR_W);

        public static Tensor operator +(Tensor a, Tensor b)
        {
            if (a.IsPoint() && b.IsPoint())
            {
                throw new InvalidOperationException(String.Format("Cannot add two points {0} and {1}", a, b));
            }

            return new Tensor(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Tensor operator -(Tensor a, Tensor b)
        {
            if (a.IsVector() && b.IsPoint())
            {
                throw new InvalidOperationException(string.Format("Cannot subtract a point from a vector ({0} - {1})", a, b));
            }

            return new Tensor(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Tensor operator -(Tensor a)
        {
            if (a.IsPoint())
            {
                throw new InvalidOperationException(string.Format("Cannot negate point {0}", a));
            }

            return Vector(-a.x, -a.y, -a.z);
        }

        public static Tensor operator *(Tensor a, float f) => new Tensor(a.x * f, a.y * f, a.z * f, a.w * f);
        public static Tensor operator *(float f, Tensor a) => a * f;
        public static Tensor operator /(Tensor t, float f) => t * (1 / f);

        public float Magnitude() => MathF.Sqrt(MathF.Pow(x, 2) + MathF.Pow(y, 2) + MathF.Pow(z, 2) + MathF.Pow(w, 2));

        public readonly Tensor Normalize()
        {
            float norm = Magnitude();
            return new Tensor(x / norm, y / norm, z / norm, w / norm);
        }

        /// <summary>
        /// Dot product between tensors a and b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float operator *(Tensor a, Tensor b)
        {
            if (a.IsPoint() || b.IsPoint())
            {
                throw new InvalidOperationException(String.Format("Can only take the dot product of two vectors, not {0} and {1}", a, b));
            }

            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public readonly Tensor Cross(Tensor t)
        {
            if (IsPoint() || t.IsPoint())
            {
                throw new InvalidOperationException(string.Format("Can only take the cross product of two vectors, not {0} and {1}", this, t));
            }

            return Vector(y * t.z - z * t.y,
                          z * t.x - x * t.z,
                          x * t.y - y * t.x);
        }
    }
}
