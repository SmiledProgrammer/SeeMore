using System;

namespace SeeMore
{
    public abstract class GenericImage<T> : Image // TODO: remove "public"
    {
        public GenericImage(uint width, uint height) : base(width, height)
        {}

        public override Image Filter(Filter filter, Neighborhood neighborhood)
        {
            Func<T[,], uint, uint, T> neighborhoodFunction;
            switch (neighborhood)
            {
                case Neighborhood.SKIP_UNDEFINED:
                    //neighborhoodFunction = (p, x, y) => (x < 0 || x >= Width || y < 0 || y >= Height) ? 0 : p[x, y];
                    break;
                case Neighborhood.ZERO_PADDING:

                    break;
                case Neighborhood.MIRROR_EXTENSION:

                    break;
            }
            return null;
        }
    }
}
