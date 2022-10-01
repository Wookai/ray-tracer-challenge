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
            var projectile = new Projectile(Tensor.Point(0, 1, 0), Tensor.Vector(1, 1, 0).Normalize() * 3);
            var enviroment = new Enviroment(Tensor.Vector(0, -0.1f, 0), Tensor.Vector(-0.01f, 0, 0));

            while (projectile.position.y >= 0)
            {
                Console.WriteLine(projectile);
                projectile = Tick(enviroment, projectile);
            }
        }
    }
}
