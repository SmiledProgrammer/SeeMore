namespace SeeMore
{
    public abstract class Channel
    {
        protected uint Width, Height;

        internal Channel(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public abstract Channel Clone();
        public abstract void Multiply(double factor);
        public abstract byte[,] ToByteArray();
    }
}
