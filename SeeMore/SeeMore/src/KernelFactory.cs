namespace SeeMore
{
    public static class KernelFactory
    {
        public static Kernel Neighborhood(KernelSize size = KernelSize.SIZE_3x3)
        {
            return new Kernel(size);
        }

        public static Kernel SobelX()
        {
            short[,] kernelData =
            {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }
            };
            return new Kernel(kernelData, 3);
        }

        public static Kernel SobelY()
        {
            short[,] kernelData =
            {
                { -1, -2, -1 },
                {  0,  0,  0 },
                {  1,  2,  1 }
            };
            return new Kernel(kernelData, 3);
        }
    }
}
