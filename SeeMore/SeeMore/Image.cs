using System;

namespace SeeMore
{
    public abstract class Image
    {
        protected uint Width, Height;

        public Image(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public abstract Image Filter(Filter filter, Neighborhood neighborhood);
        public abstract Image Intensify(); //tmp
    }
}
