using System.Text;

namespace RayTracerChallenge
{
    public class Canvas
    {
        private readonly Color[,] pixels;
        public int Height;
        public int Width;

        public Canvas(int width, int height)
        {
            Height = height;
            Width = width;
            pixels = new Color[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pixels[x, y] = new Color(0, 0, 0);
                }
            }
        }

        public void WritePixel(int x, int y, Color c) => pixels[x, y] = c;
        public Color GetPixel(int x, int y) => pixels[x, y];

        public string ToPPM()
        {
            StringBuilder sb = new();
            sb.Append("P3\n");
            sb.AppendFormat("{0} {1}\n", Width, Height);
            sb.Append("255\n");
            return sb.ToString();
        }
    }
}

