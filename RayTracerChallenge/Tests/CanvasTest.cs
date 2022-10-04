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
            Assert.AreEqual("P3\n5 3\n255\n", canvas.ToPPM());
        }
    }
}
