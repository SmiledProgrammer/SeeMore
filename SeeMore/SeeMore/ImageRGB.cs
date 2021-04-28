using System;

namespace SeeMore
{
    public abstract class ImageRGB<T> : GenericImage<T> // TODO: remove "public"
    {
        public Channel<T> R { get; protected set; }
        public Channel<T> G { get; protected set; }
        public Channel<T> B { get; protected set; }

        public ImageRGB(uint width, uint height, Channel<T> r, Channel<T> g, Channel<T> b) : base(width, height)
        {
            R = r;
            G = g;
            B = b;
        }

        protected void ApplyToChannels(Func<T[,], int, int, T> operation) //check if needed l8r
        {
            R.Apply(operation);
            G.Apply(operation);
            B.Apply(operation);
        }
    }
}
