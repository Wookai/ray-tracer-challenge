namespace RayTracerChallenge
{
    struct Projectile
    {
        public Tensor position;
        public Tensor velocity;
        public Projectile(Tensor p, Tensor v)
        {
            position = p;
            velocity = v;
        }
        public override string ToString()
        {
            return string.Format("Position: {0}, Velocity: {1}", position, velocity);
        }
    }

    struct Enviroment
    {
        public Tensor gravity;
        public Tensor wind;
        public Enviroment(Tensor g, Tensor w)
        {
            gravity = g;
            wind = w;
        }
    }

    class Cannon
    {
        static Projectile Tick(Enviroment env, Projectile proj)
        {
            var position = proj.position + proj.velocity;
            var velocity = proj.velocity + env.gravity + env.wind;
            return new Projectile(position, velocity);
        }

        static void Main(string[] args)
        {
            var start = Tensor.Point(0, 1, 0);
            var velocity = Tensor.Vector(1, 1.8f, 0).Normalize() * 11.25f;
            var projectile = new Projectile(start, velocity);

            var gravity = Tensor.Vector(0, -0.1f, 0);
            var wind = Tensor.Vector(-0.01f, 0, 0);
            var environment = new Enviroment(gravity, wind);

            int width = 900;
            int height = 550;
            var canvas = new Canvas(width, height);

            var color = new Color(0, 1, 0);

            while (projectile.position.y >= 0)
            {
                int x = (int)Math.Round(projectile.position.x);
                int y = (int)Math.Round(height - projectile.position.y);

                if (0 <= x && x < width && 0 <= y && y < height)
                {
                    canvas.WritePixel(x, y, color);
                }
                projectile = Tick(environment, projectile);
            }

            File.WriteAllText("image.ppm", canvas.ToPPM());
        }
    }
}
