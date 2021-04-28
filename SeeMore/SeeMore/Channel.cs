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

        public abstract T Mean();
        public abstract void Apply(Func<T[,], int, int, T> operation);
        public abstract void Print(); //tmp
        public abstract void Randomize(); //tmp
    }
}
