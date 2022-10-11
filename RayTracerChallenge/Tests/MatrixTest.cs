using RayTracerChallenge;

namespace Tests
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void Create4x4Test()
        {
            var m = new Matrix(new float[4, 4] { { 1, 2, 3, 4 }, { 5.5f, 6.5f, 7.5f, 8.5f }, { 9, 10, 11, 12 }, { 13.5f, 14.5f, 15.5f, 16.5f } });
            Assert.AreEqual(1, m[0, 0]);
            Assert.AreEqual(2, m[0, 1]);
            Assert.AreEqual(3, m[0, 2]);
            Assert.AreEqual(4, m[0, 3]);
            Assert.AreEqual(5.5, m[1, 0]);
            Assert.AreEqual(6.5, m[1, 1]);
            Assert.AreEqual(7.5, m[1, 2]);
            Assert.AreEqual(8.5, m[1, 3]);
            Assert.AreEqual(9, m[2, 0]);
            Assert.AreEqual(10, m[2, 1]);
            Assert.AreEqual(11, m[2, 2]);
            Assert.AreEqual(12, m[2, 3]);
            Assert.AreEqual(13.5, m[3, 0]);
            Assert.AreEqual(14.5, m[3, 1]);
            Assert.AreEqual(15.5, m[3, 2]);
            Assert.AreEqual(16.5, m[3, 3]);
        }

        [TestMethod]
        public void Create2x2Test()
        {
            var m = new Matrix(new float[2, 2] { { -3, 5 }, { 1, -2 } });
            Assert.AreEqual(-3, m[0, 0]);
            Assert.AreEqual(5, m[0, 1]);
            Assert.AreEqual(1, m[1, 0]);
            Assert.AreEqual(-2, m[1, 1]);
        }

        [TestMethod]
        public void Create3x3Test()
        {
            var m = new Matrix(new float[3, 3] { { -3, 5, 0 }, { 1, -2, -7 }, { 0, 1, 1 } });
            Assert.AreEqual(-3, m[0, 0]);
            Assert.AreEqual(5, m[0, 1]);
            Assert.AreEqual(0, m[0, 2]);
            Assert.AreEqual(1, m[1, 0]);
            Assert.AreEqual(-2, m[1, 1]);
            Assert.AreEqual(-7, m[1, 2]);
            Assert.AreEqual(0, m[2, 0]);
            Assert.AreEqual(1, m[2, 1]);
            Assert.AreEqual(1, m[2, 2]);
        }

        [TestMethod]
        public void EqualityTest()
        {
            var m1 = new Matrix(new float[2, 2] { { -3, 5 }, { 1, -2 } });
            var m2 = new Matrix(new float[3, 3] { { -3, 5, 0 }, { 1, -2, -7 }, { 0, 1, 1 } });
            var m3 = new Matrix(new float[3, 3] { { -3, 5, 0 }, { 1, -2, -7 }, { 0, 1, 11 } });
            var m4 = new Matrix(new float[3, 3] { { -3, 5, 0 }, { 1, -2, -7 }, { 0, 1, 11 } });
            Assert.IsTrue(m1 != m2);
            Assert.IsTrue(m2 != m3);
            Assert.IsTrue(m3 == m4);
        }

        [TestMethod]
        public void MatrixMultiplicationTest()
        {
            var a = new Matrix(new float[4, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 8, 7, 6 }, { 5, 4, 3, 2 } });
            var b = new Matrix(new float[4, 4] { { -2, 1, 2, 3 }, { 3, 2, 1, -1 }, { 4, 3, 6, 5 }, { 1, 2, 7, 8 } });
            Assert.AreEqual(new Matrix(new float[4, 4] { { 20, 22, 50, 48 }, { 44, 54, 114, 108 }, { 40, 58, 110, 102 }, { 16, 26, 46, 42 } }), a * b);
            Assert.ThrowsException<ArgumentException>(() => new Matrix(1, 2) * new Matrix(1, 2));
        }

        [TestMethod]
        public void VectorMultiplicationTest()
        {
            var a = new Matrix(new float[4, 4] { { 1, 2, 3, 4 }, { 2, 4, 4, 2 }, { 8, 6, 4, 1 }, { 0, 0, 0, 1 } });
            var b = new Tensor(1, 2, 3, 1);
            Assert.AreEqual(new Tensor(18, 24, 33, 1), a * b);
            Assert.ThrowsException<ArgumentException>(() => new Matrix(4, 2) * b);
        }

        [TestMethod]
        public void IdentityTest()
        {
            var a = new Matrix(new float[4, 4] { { 1, 2, 3, 4 }, { 2, 4, 4, 2 }, { 8, 6, 4, 1 }, { 0, 0, 0, 1 } });
            Assert.AreEqual(a, Matrix.Identity() * a);
            Assert.AreEqual(a, a * Matrix.Identity());
            var b = new Tensor(1, 2, 3, 4);
            Assert.AreEqual(b, Matrix.Identity() * b);
        }

        [TestMethod]
        public void TransposeTest()
        {
            var a = new Matrix(new float[4, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } });
            Assert.AreEqual(new Matrix(new float[4, 4] { { 1, 5, 9, 13 }, { 2, 6, 10, 14 }, { 3, 7, 11, 15 }, { 4, 8, 12, 16 } }), a.Transpose());
            Assert.AreEqual(Matrix.Identity(), Matrix.Identity().Transpose());
        }

        [TestMethod]
        public void DeterminantTest()
        {
            Assert.AreEqual(17, new Matrix(new float[2, 2] { { 1, 5 }, { -3, 2 } }).Determinant());
            Assert.ThrowsException<InvalidOperationException>(() => new Matrix(2, 3).Determinant());
        }

        [TestMethod]
        public void SubmatrixTest()
        {
            Assert.AreEqual(new Matrix(new float[2, 2] { { -3, 2 }, { 0, 6 } }),
                new Matrix(new float[3, 3] { { 1, 5, 0 }, { -3, 2, 7 }, { 0, 6, -3 } }).Submatrix(0, 2));
            Assert.AreEqual(new Matrix(new float[3, 3] { { -6, 1, 6 }, { -8, 8, 6 }, { -7, -1, 1 } }),
                new Matrix(new float[4, 4] { { -6, 1, 1, 6 }, { -8, 5, 8, 6 }, { -1, 0, 8, 2 }, { -7, 1, -1, 1 } }).Submatrix(2, 1));
        }
    }
}
