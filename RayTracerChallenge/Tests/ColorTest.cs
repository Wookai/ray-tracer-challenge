using RayTracerChallenge;

namespace Tests
{
    [TestClass]
    public class ColorTest
    {
        [TestMethod]
        public void TestCreateColor()
        {
            var c = new Color(-0.5f, 0.4f, 1.7f);
            Assert.AreEqual(-0.5, c.red, 1e-3);
            Assert.AreEqual(0.4, c.green, 1e-3);
            Assert.AreEqual(1.7, c.blue, 1e-3);
        }

        [TestMethod]
        public void TestEquality()
        {
            var a = new Color(-0.5f, 0.4f, 1.7f);
            var b = new Color(-0.5f, 0.4f, 1.7f);
            var c = new Color(1, 2, 3);
            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsTrue(a == b);
            Assert.IsTrue(a != c);
        }

        [TestMethod]
        public void TestAddition()
        {
            var sum = new Color(0.9f, 0.6f, 0.75f) + new Color(0.7f, 0.1f, 0.25f);
            Assert.AreEqual(1.6, sum.red, 1e-3);
            Assert.AreEqual(0.7, sum.green, 1e-3);
            Assert.AreEqual(1.0, sum.blue, 1e-3);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            var difference = new Color(0.9f, 0.6f, 0.75f) - new Color(0.7f, 0.1f, 0.15f);
            Assert.AreEqual(0.2, difference.red, 1e-3);
            Assert.AreEqual(0.5, difference.green, 1e-3);
            Assert.AreEqual(0.6, difference.blue, 1e-3);
        }

        [TestMethod]
        public void TestMultiplication()
        {
            var c = new Color(0.2f, 0.3f, 0.4f);
            var twoC = 2 * c;
            Assert.AreEqual(0.4, twoC.red, 1e-3);
            Assert.AreEqual(0.6, twoC.green, 1e-3);
            Assert.AreEqual(0.8, twoC.blue, 1e-3);

            var product = new Color(1, 0.2f, 0.4f) * new Color(0.9f, 1, 0.1f);
            Assert.AreEqual(0.9f, product.red, 1e-3);
            Assert.AreEqual(0.2f, product.green, 1e-3);
            Assert.AreEqual(0.04f, product.blue, 1e-3);
        }
    }
}
