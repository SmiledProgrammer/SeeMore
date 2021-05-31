namespace SeeMore
{
    public class Kernel
    {
        public uint Size { get; }
        private byte[,] ConvolutionMatrix;

        internal Kernel(KernelSize size)
        {
            Size = (uint)size;
            ConvolutionMatrix = new byte[Size, Size];
            for (uint x = 0; x < Size; x++)
            {
                for (uint y = 0; y < Size; y++)
                {
                    ConvolutionMatrix[x, y] = 1;
                }
            }
        }

        internal Kernel(byte[,] convolutionMatrix, uint size)
        {
            Size = size;
            ConvolutionMatrix = convolutionMatrix.Clone() as byte[,];
        }

        public byte this[uint x, uint y]
        {
            get => ConvolutionMatrix[x, y];
            set => ConvolutionMatrix[x, y] = value;
        }
    }
}
