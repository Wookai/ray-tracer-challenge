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

        private readonly int MAX_LINE_LENGTH = 70;

        public string ToPPM()
        {
            StringBuilder sb = new();
            sb.Append("P3\n");
            sb.AppendFormat("{0} {1}\n", Width, Height);
            sb.Append("255\n");
            for (int y = 0; y < Height; y++)
            {
                var line = string.Join(' ', Enumerable.Range(0, Width).Select(i => pixels[i, y].ToRGB()));
                while (line.Length > MAX_LINE_LENGTH)
                {
                    int lastSpacePosition = line[..MAX_LINE_LENGTH].LastIndexOf(' ');
                    sb.Append(line[..lastSpacePosition] + '\n');
                    line = line[(lastSpacePosition + 1)..];
                }
                sb.Append(line + '\n');
            }
            return sb.ToString();
        }
    }
}

