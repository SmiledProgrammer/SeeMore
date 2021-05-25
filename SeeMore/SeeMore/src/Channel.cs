namespace SeeMore
{
    public abstract class Channel<T>
    {
        protected uint Width, Height;
        public T[,] Pixels { get; set; }

        public Channel(uint width, uint height)
        {
            Width = width;
            Height = height;
            Pixels = new T[width, height];
        }

        public abstract Channel<T> Clone();
        public abstract byte[,] ToByteArray();

        public abstract void Average(Channel<T> originalChannel, Image<T>.NeighborhoodFunction neighborhoodFunction, uint x, uint y, uint neighborhoodSize);
        public abstract void Median(Channel<T> originalChannel, Image<T>.NeighborhoodFunction neighborhoodFunction, uint x, uint y, uint neighborhoodSize);
        public abstract void Maximum(Channel<T> originalChannel, Image<T>.NeighborhoodFunction neighborhoodFunction, uint x, uint y, uint neighborhoodSize);
        public abstract void Minimum(Channel<T> originalChannel, Image<T>.NeighborhoodFunction neighborhoodFunction, uint x, uint y, uint neighborhoodSize);
        public abstract void Range(Channel<T> originalChannel, Image<T>.NeighborhoodFunction neighborhoodFunction, uint x, uint y, uint neighborhoodSize);

        public T this[uint x, uint y]
        {
            get => Pixels[x, y];
            set => Pixels[x, y] = value;
        }
    }
}
