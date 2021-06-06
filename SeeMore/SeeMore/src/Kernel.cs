namespace SeeMore
{
    public class Kernel
    {
        public uint Size { get; }
        private short[,] ConvolutionMatrix;

        internal Kernel(KernelSize size)
        {
            Size = (uint)size;
            ConvolutionMatrix = new short[Size, Size];
            for (uint x = 0; x < Size; x++)
            {
                for (uint y = 0; y < Size; y++)
                {
                    ConvolutionMatrix[x, y] = 1;
                }
            }
        }

        internal Kernel(short[,] convolutionMatrix, uint size)
        {
            Size = size;
            ConvolutionMatrix = convolutionMatrix.Clone() as short[,];
        }

        public short this[uint x, uint y]
        {
            get => ConvolutionMatrix[x, y];
            set => ConvolutionMatrix[x, y] = value;
        }
    }
}
