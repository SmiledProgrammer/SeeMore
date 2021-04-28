using System;

namespace SeeMore
{
    public abstract class GenericImage<T> : Image // TODO: remove "public"
    {
        public GenericImage(uint width, uint height) : base(width, height)
        {}

        public override Image Filter(FilterType filter, NeighborhoodSize neighborhoodSize, NeighborhoodType neighborhoodType)
        {
            uint size = (uint)neighborhoodSize;
            uint range = size / 2;
            if (Width < range || Height < range)
            {
                throw new IndexOutOfRangeException("Neighborhood range cannot be greater than image size.");
            }
            //Func<T> filterOperation;
            Func<T[,], int, int, T> neighborhoodFunction = NeighborhoodFunction(neighborhoodType);
            uint lowerX, upperX, lowerY, upperY;
            if (neighborhoodType == NeighborhoodType.SKIP_UNDEFINED)
            {
                lowerX = range;
                upperX = Width - range;
                lowerY = range;
                upperY = Height - range;
            }
            else
            {
                lowerX = 0;
                upperX = Width;
                lowerY = 0;
                upperY = Height;
            }
            for (uint x = lowerX; x < upperX; x++)
            {
                for (uint y = lowerY; y < upperY; y++)
                {

                }
            }
            return null; // TODO: change
        }

        private Func<T[,], int, int, T> NeighborhoodFunction(NeighborhoodType neighborhoodType) // TODO: zrobić for'y zliczające zamiast zwracania pojedynczych wartości
        {
            if (neighborhoodType == NeighborhoodType.MIRROR_EXTENSION)
            {
                return (p, x, y) =>
                {
                    int finalX, finalY;
                    if (x < 0)
                        finalX = -x;
                    else if (x >= Width)
                        finalX = (int)(Width - (x - Width + 1));
                    else
                        finalX = x;
                    if (y < 0)
                        finalY = -y;
                    else if (y >= Height)
                        finalY = (int)(Height - (y - Height + 1));
                    else
                        finalY = y;
                    return p[finalX, finalY];
                };
            }
            else
            {
                return (p, x, y) => (x < 0 || x >= Width || y < 0 || y >= Height) ? default : p[x, y];
            }
        }
    }
}
