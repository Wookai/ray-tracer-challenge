using System.Diagnostics.CodeAnalysis;

namespace RayTracerChallenge
{
    public readonly struct Color
    {
        public readonly float red;
        public readonly float green;
        public readonly float blue;

        public Color(float r, float g, float b) { red = r; green = g; blue = b; }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Color c = (Color)obj;
            return c.red == red && c.green == green && c.blue == blue;
        }

        public static bool operator ==(Color left, Color right) => left.Equals(right);
        public static bool operator !=(Color left, Color right) => !(left == right);

        public override int GetHashCode() => Tuple.Create(red, green, blue).GetHashCode();

        public static Color operator +(Color a, Color b) => new(a.red + b.red, a.green + b.green, a.blue + b.blue);
        public static Color operator -(Color a, Color b) => new(a.red - b.red, a.green - b.green, a.blue - b.blue);
        public static Color operator *(Color a, float b) => new(a.red * b, a.green * b, a.blue * b);
        public static Color operator *(float a, Color b) => b * a;
        public static Color operator *(Color a, Color b) => new(a.red * b.red, a.green * b.green, a.blue * b.blue);

    }
}
