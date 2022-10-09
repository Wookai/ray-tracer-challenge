using RayTracerChallenge;

namespace Tests
{
    [TestClass]
    public class CanvasTest
    {
        [TestMethod]
        public void TestCreate()
        {
            var canvas = new Canvas(10, 20);
            Assert.AreEqual(10, canvas.Width);
            Assert.AreEqual(20, canvas.Height);
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    Assert.AreEqual(new Color(0, 0, 0), canvas.GetPixel(x, y));
                }
            }
        }

        [TestMethod]
        public void TestWritePixel()
        {
            var canvas = new Canvas(10, 20);
            var red = new Color(1, 0, 0);
            canvas.WritePixel(2, 3, red);
            Assert.AreEqual(red, canvas.GetPixel(2, 3));
        }

        [TestMethod]
        public void TestToPPM()
        {
            var canvas = new Canvas(5, 3);
            canvas.WritePixel(0, 0, new Color(1.5f, 0, 0));
            canvas.WritePixel(2, 1, new Color(0, 0.5f, 0));
            canvas.WritePixel(4, 2, new Color(-0.5f, 0, 1));
            Assert.AreEqual("P3\n5 3\n255\n255 0 0 0 0 0 0 0 0 0 0 0 0 0 0\n0 0 0 0 0 0 0 128 0 0 0 0 0 0 0\n0 0 0 0 0 0 0 0 0 0 0 0 0 0 255\n", canvas.ToPPM());
        }

        [TestMethod]
        public void TestToPPMWrapsLines()
        {
            int width = 10;
            int height = 2;
            var canvas = new Canvas(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    canvas.WritePixel(x, y, new Color(1, 0.8f, 0.6f));
                }
            }
            Assert.AreEqual("P3\n10 2\n255\n255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n153 255 204 153 255 204 153 255 204 153 255 204 153\n255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n153 255 204 153 255 204 153 255 204 153 255 204 153\n", canvas.ToPPM());
        }
    }
}
