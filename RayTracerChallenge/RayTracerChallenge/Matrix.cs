using System.Diagnostics.CodeAnalysis;

namespace RayTracerChallenge
{
    public struct Matrix
    {
        public readonly float[,] Data;
        public readonly int Rows;
        public readonly int Columns;

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Data = new float[rows, columns];
        }

        public Matrix(float[,] data)
        {
            Rows = data.GetLength(0);
            Columns = data.GetLength(1);
            Data = data;
        }

        public static Matrix Identity()
        {
            return new Matrix(new float[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } });
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Matrix other = (Matrix)obj;
            return other.Rows == Rows && other.Columns == Columns && Data.Cast<float>().SequenceEqual(other.Data.Cast<float>());
        }
        public static bool operator ==(Matrix left, Matrix right) => left.Equals(right);
        public static bool operator !=(Matrix left, Matrix right) => !(left == right);

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        public float this[int x, int y] => Data[x, y];

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.Columns != b.Rows)
            {
                throw new ArgumentException(string.Format("Cannot multiply {0}x{1} matrix with {2}x{3} matrix.", a.Rows, a.Columns, b.Rows, b.Columns));
            }

            var product = new Matrix(a.Rows, b.Columns);
            for (int row = 0; row < product.Rows; row++)
            {
                for (int col = 0; col < product.Columns; col++)
                {
                    float value = 0;
                    for (int i = 0; i < a.Columns; i++)
                    {
                        value += a[row, i] * b[i, col];
                    }
                    product.Data[row, col] = value;
                }
            }

            return product;
        }

        public static Tensor operator *(Matrix a, Tensor b)
        {
            if (a.Rows != 4 || a.Columns != 4)
            {
                throw new ArgumentException(string.Format("Only 4x4 matrices can be multiplied with Tensors, shape was {0}x{1}", a.Rows, a.Columns));
            }

            var vector = new float[4] { b.x, b.y, b.z, b.w };
            var values = new float[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    values[i] += a[i, j] * vector[j];
                }
            }

            return new Tensor(values[0], values[1], values[2], values[3]);
        }

        public Matrix Transpose()
        {
            var transpose = new Matrix(Columns, Rows);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    transpose.Data[j, i] = Data[i, j];
                }
            }
            return transpose;
        }

        public float Determinant()
        {
            if (Rows != 2 || Columns != 2)
            {
                throw new InvalidOperationException(string.Format("Only determinant of 2x2 matrices is implemented, tried to compute determinant of {0}x{1} matrix", Rows, Columns));
            }
            return Data[0, 0] * Data[1, 1] - Data[0, 1] * Data[1, 0];
        }

        /// <summary>
        /// Returns a copy of this matrix with row <paramref name="row"/> and column <paramref name="column"/> removed.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Matrix Submatrix(int row, int column)
        {
            var submatrix = new Matrix(Rows - 1, Columns - 1);
            for (int i = 0; i < Rows; i++)
            {
                if (i == row)
                {
                    continue;
                }

                for (int j = 0; j < Columns; j++)
                {
                    if (j == column)
                    {
                        continue;
                    }

                    submatrix.Data[i >= row ? i - 1 : i, j >= column ? j - 1 : j] = Data[i, j];
                }
            }
            return submatrix;
        }
    }
}
