using System;

namespace SeeMore
{
    public static class KernelFactory
    {
        public static Kernel FromMatrix(double[,] matrix)
        {
            int sizeX = matrix.GetLength(0);
            int sizeY = matrix.GetLength(1);
            if (sizeX != sizeY)
            {
                throw new IndexOutOfRangeException("The given matrix is not a square matrix.");
            }
            return new Kernel(matrix, (uint)sizeX);
        }

        public static Kernel Neighborhood(KernelSize size = KernelSize.SIZE_3x3)
        {
            return new Kernel(size);
        }

        public static Kernel SobelVertical()
        {
            double[,] kernelData =
            {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }
            };
            return new Kernel(kernelData, 3);
        }

        public static Kernel SobelHorizontal()
        {
            double[,] kernelData =
            {
                { -1, -2, -1 },
                {  0,  0,  0 },
                {  1,  2,  1 }
            };
            return new Kernel(kernelData, 3);
        }

        public static Kernel Gaussian3x3()
        {
            double[,] kernelData =
            {
                { 1, 2, 1 },
                { 2, 4, 2 },
                { 1, 2, 1 }
            };
            return new Kernel(kernelData, 3).Multiply(1.0 / 16.0);
        }

        public static Kernel Gaussian5x5()
        {
            double[,] kernelData =
            {
                { 1,  4,  6,  4, 1 },
                { 4, 16, 24, 16, 4 },
                { 6, 24, 36, 24, 6 },
                { 4, 16, 24, 16, 4 },
                { 1,  4,  6,  4, 1 }
            };
            return new Kernel(kernelData, 5).Multiply(1.0 / 256.0);
        }
    }
}
