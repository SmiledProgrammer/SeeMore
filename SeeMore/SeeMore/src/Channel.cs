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

        public abstract void Average(Channel<T> originalChannel, GenericImage<T>.NeighborhoodFunction neighborhoodFunction, uint x, uint y);
        //public abstract void Maximum(Channel<T> originalChannel, NeighborhoodFunction neighborhoodFunction, uint x, uint y);
        //public abstract void Minimum(Channel<T> originalChannel, NeighborhoodFunction neighborhoodFunction, uint x, uint y);
        public abstract void Median(Channel<T> originalChannel, GenericImage<T>.NeighborhoodFunction neighborhoodFunction, uint x, uint y, uint neighborhoodSize);
        //public abstract void Diversity(Channel<T> originalChannel, NeighborhoodFunction neighborhoodFunction, uint x, uint y);
        //public abstract void Range(Channel<T> originalChannel, NeighborhoodFunction neighborhoodFunction, uint x, uint y);
    }
}
