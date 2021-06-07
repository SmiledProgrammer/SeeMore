namespace SeeMore
{
    public class Kernel
    {
        public uint Size { get; }
        private double[,] ConvolutionMatrix;

        internal Kernel(KernelSize size)
        {
            Size = (uint)size;
            ConvolutionMatrix = new double[Size, Size];
            uint count = Size * Size;
            for (uint x = 0; x < Size; x++)
            {
                for (uint y = 0; y < Size; y++)
                {
                    ConvolutionMatrix[x, y] = 1.0 / count;
                }
            }
        }

        internal Kernel(double[,] convolutionMatrix, uint size)
        {
            Size = size;
            ConvolutionMatrix = convolutionMatrix.Clone() as double[,];
        }

        internal Kernel Multiply(double factor)
        {
            Kernel multipliedKernel = new Kernel(ConvolutionMatrix, Size);
            for (uint x = 0; x < Size; x++)
            {
                for (uint y = 0; y < Size; y++)
                {
                    multipliedKernel.ConvolutionMatrix[x, y] = ConvolutionMatrix[x, y] * factor;
                }
            }
            return multipliedKernel;
        }

        public double this[uint x, uint y]
        {
            get => ConvolutionMatrix[x, y];
            set => ConvolutionMatrix[x, y] = value;
        }
    }
}
