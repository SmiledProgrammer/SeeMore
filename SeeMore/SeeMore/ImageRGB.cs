using System;

namespace SeeMore
{
    public abstract class ImageRGB<T> : GenericImage<T> // TODO: remove "public"
    {
        public Channel<T> R { get; protected set; }
        public Channel<T> G { get; protected set; }
        public Channel<T> B { get; protected set; }

        protected ImageRGB(uint width, uint height, Channel<T> r, Channel<T> g, Channel<T> b) : base(width, height)
        {
            R = r;
            G = g;
            B = b;
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.RGB;
        }
    }
}
