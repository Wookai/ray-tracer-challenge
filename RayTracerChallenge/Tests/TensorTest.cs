using RayTracerChallenge;

namespace Tests
{
    [TestClass]
    public class TensorTest
    {
        [TestMethod]
        public void TestCreatePoint()
        {
            var t = new Tensor(4.3f, -4.2f, 3.1f, 1.0f);
            Assert.AreEqual(4.3f, t.x);
            Assert.AreEqual(-4.2f, t.y);
            Assert.AreEqual(3.1f, t.z);
            Assert.AreEqual(1.0f, t.w);
            Assert.IsTrue(t.IsPoint());
            Assert.IsFalse(t.IsVector());
        }

        [TestMethod]
        public void TestCreateVector()
        {
            var t = new Tensor(4.3f, -4.2f, 3.1f, 0.0f);
            Assert.AreEqual(4.3f, t.x);
            Assert.AreEqual(-4.2f, t.y);
            Assert.AreEqual(3.1f, t.z);
            Assert.AreEqual(0.0f, t.w);
            Assert.IsTrue(t.IsVector());
            Assert.IsFalse(t.IsPoint());
        }

        [TestMethod]
        public void TestTensorEquality()
        {
            var t = new Tensor(1, 2, 3, 0);
            Assert.AreEqual(new Tensor(1, 2, 3, 0), t);
            Assert.AreNotEqual(new Tensor(4, 2, 3, 0), t);
            Assert.AreNotEqual(new Tensor(1, 4, 3, 0), t);
            Assert.AreNotEqual(new Tensor(1, 2, 4, 0), t);
            Assert.AreNotEqual(new Tensor(1, 2, 3, 4), t);
        }

        [TestMethod]
        public void TestCreatePointUsingHelper()
        {
            var t = Tensor.Point(4, -4, 3);
            Assert.AreEqual(new Tensor(4, -4, 3, 1), t);
        }

        [TestMethod]
        public void TestCreateVectorUsingHelper()
        {
            var v = Tensor.Vector(4, -4, 3);
            Assert.AreEqual(new Tensor(4, -4, 3, 0), v);
        }

        [TestMethod]
        public void TestAddition()
        {
            var p = Tensor.Point(1, 2, 3);
            var v = Tensor.Vector(4, 5, 6);
            var sum = Tensor.Point(5, 7, 9);
            Assert.AreEqual(sum, p + v);
            Assert.AreEqual(sum, v + p);
            Assert.IsTrue((p + v).IsPoint());
            Assert.IsTrue((v + p).IsPoint());
            Assert.AreEqual(Tensor.Vector(8, 10, 12), v + v);
            Assert.IsTrue((v + v).IsVector());
            Assert.ThrowsException<InvalidOperationException>(() => p + p);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            var p1 = Tensor.Point(1, 2, 3);
            var p2 = Tensor.Point(10, 20, 30);
            var v1 = Tensor.Vector(6, 5, 4);
            var v2 = Tensor.Vector(60, 50, 40);
            Assert.AreEqual(Tensor.Vector(-9, -18, -27), p1 - p2);
            Assert.AreEqual(Tensor.Point(-5, -3, -1), p1 - v1);
            Assert.AreEqual(Tensor.Vector(-54, -45, -36), v1 - v2);
            Assert.ThrowsException<InvalidOperationException>(() => v1 - p1);
        }

        [TestMethod]
        public void TestNegation()
        {
            var v = Tensor.Vector(6, 5, 4);
            var minus_v = Tensor.Vector(-6, -5, -4);
            Assert.AreEqual(minus_v, Tensor.Vector(0, 0, 0) - v);
            Assert.AreEqual(minus_v, -v);
            Assert.ThrowsException<InvalidOperationException>(() => -Tensor.Point(1, 2, 3));
        }

        [TestMethod]
        public void TestMultiplication()
        {
            var t = new Tensor(1, 2, 3, 4);
            Assert.AreEqual(new Tensor(3, 6, 9, 12), 3 * t);
            Assert.AreEqual(new Tensor(0.5f, 1, 1.5f, 2), t * 0.5f);
        }

        [TestMethod]
        public void TestDivision()
        {
            Assert.AreEqual(new Tensor(0.5f, 1, 1.5f, 2), new Tensor(1, 2, 3, 4) / 2);
        }

        [TestMethod]
        public void TestMagnitude()
        {
            Assert.AreEqual(1, Tensor.Vector(1, 0, 0).Magnitude());
            Assert.AreEqual(1, Tensor.Vector(0, 1, 0).Magnitude());
            Assert.AreEqual(1, Tensor.Vector(0, 0, 1).Magnitude());
            Assert.AreEqual(MathF.Sqrt(14), Tensor.Vector(1, 2, 3).Magnitude());
            Assert.AreEqual(MathF.Sqrt(14), Tensor.Vector(-1, -2, -3).Magnitude());
        }

        [TestMethod]
        public void TestNormalize()
        {
            Assert.AreEqual(Tensor.Vector(1, 0, 0), Tensor.Vector(4, 0, 0).Normalize());
            Assert.AreEqual(Tensor.Vector(1 / MathF.Sqrt(14), 2 / MathF.Sqrt(14), 3 / MathF.Sqrt(14)), Tensor.Vector(1, 2, 3).Normalize());
            Assert.AreEqual(1, Tensor.Vector(1, 2, 3).Normalize().Magnitude(), 1e-5);
        }

        [TestMethod]
        public void TestDotProduct()
        {
            Assert.AreEqual(20, Tensor.Vector(1, 2, 3) * Tensor.Vector(2, 3, 4));
            Assert.ThrowsException<InvalidOperationException>(() => Tensor.Vector(1, 2, 3) * Tensor.Point(2, 3, 4));
            Assert.ThrowsException<InvalidOperationException>(() => Tensor.Point(1, 2, 3) * Tensor.Vector(2, 3, 4));
            Assert.ThrowsException<InvalidOperationException>(() => Tensor.Point(1, 2, 3) * Tensor.Point(2, 3, 4));
        }

        [TestMethod]
        public void TestCrossProduct()
        {
            var a = Tensor.Vector(1, 2, 3);
            var b = Tensor.Vector(2, 3, 4);
            Assert.AreEqual(Tensor.Vector(-1, 2, -1), a.Cross(b));
            Assert.AreEqual(Tensor.Vector(1, -2, 1), b.Cross(a));
            Assert.ThrowsException<InvalidOperationException>(() => a.Cross(Tensor.Point(2, 3, 4)));
            Assert.ThrowsException<InvalidOperationException>(() => Tensor.Point(1, 2, 3).Cross(b));
            Assert.ThrowsException<InvalidOperationException>(() => Tensor.Point(1, 2, 3).Cross(Tensor.Point(2, 3, 4)));
        }
    }
}