using System;

namespace SeeMore
{
    public abstract class Channel<T>
    {
        protected uint Width, Height;
        protected T[,] Pixels;

        public Channel(uint width, uint height)
        {
            Width = width;
            Height = height;
            Pixels = new T[width, height];
        }

        public abstract void Average(Channel<T> originalChannel, Action<T[,], int, int, Action<T>> neighborhoodFunction, uint x, uint y);
        //public abstract void Maximum(Channel<T> originalChannel, Action<T[,], int, int, Action<T>> neighborhoodFunction, uint x, uint y);
        //public abstract void Minimum(Channel<T> originalChannel, Action<T[,], int, int, Action<T>> neighborhoodFunction, uint x, uint y);
        public abstract void Median(Channel<T> originalChannel, Action<T[,], int, int, Action<T>> neighborhoodFunction, uint x, uint y, uint neighborhoodSize);
        //public abstract void Diversity(Channel<T> originalChannel, Action<T[,], int, int, Action<T>> neighborhoodFunction, uint x, uint y);
        //public abstract void Range(Channel<T> originalChannel, Action<T[,], int, int, Action<T>> neighborhoodFunction, uint x, uint y);

        public abstract void Print(); //tmp
        public abstract void Randomize(); //tmp
    }
}
