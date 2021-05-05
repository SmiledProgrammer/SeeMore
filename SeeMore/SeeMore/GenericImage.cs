using System;

namespace SeeMore
{
    public abstract class GenericImage<T> : Image
    {
        protected GenericImage(uint width, uint height) : base(width, height)
        {}

        //public abstract void Sum(Image result, uint x, uint y);
        public abstract void Average(Image originalImage, Action<T[,], int, int, Action<T>> neighborhoodFunction, uint x, uint y, Image outputImage);
        //public abstract void Maximum(Image result, uint x, uint y);
        //public abstract void Minimum(Image result, uint x, uint y);
        //public abstract void Median(Image result, uint x, uint y);
        //public abstract void Diversity(Image result, uint x, uint y);
        //public abstract void Range(Image result, uint x, uint y);

        public override Image Filter(FilterType filter, NeighborhoodSize neighborhoodSize, NeighborhoodType neighborhoodType)
        {
            Image result = ImageFactory.Create(Width, Height, GetDataType(), GetColorModel());
            uint size = (uint)neighborhoodSize;
            uint range = size / 2;
            if (Width < range + 1 || Height < range + 1)
            {
                throw new IndexOutOfRangeException("Neighborhood range cannot be greater than image size.");
            }
            uint lowerX, upperX, lowerY, upperY;
            GetNeighborhoodArea(neighborhoodType, range, out lowerX, out upperX, out lowerY, out upperY);
            Action<T[,], int, int, Action<T>> neighborhoodFunction = GetNeighborhoodFunction(neighborhoodType, (int)range);
            Action<Image, Action<T[,], int, int, Action<T>>, uint, uint, Image> filterOperation = GetFilterOperation(filter);
            for (uint x = lowerX; x < upperX; x++)
            {
                for (uint y = lowerY; y < upperY; y++)
                {
                    filterOperation(this, neighborhoodFunction, x, y, result);
                }
            }
            return result;
        }

        private void GetNeighborhoodArea(NeighborhoodType neighborhoodType, uint neighborhoodRange, out uint lowerX, out uint upperX, out uint lowerY, out uint upperY)
        {
            if (neighborhoodType == NeighborhoodType.SKIP_UNDEFINED)
            {
                lowerX = neighborhoodRange;
                upperX = Width - neighborhoodRange;
                lowerY = neighborhoodRange;
                upperY = Height - neighborhoodRange;
            }
            else
            {
                lowerX = 0;
                upperX = Width;
                lowerY = 0;
                upperY = Height;
            }
        }

        private Action<T[,], int, int, Action<T>> GetNeighborhoodFunction(NeighborhoodType neighborhoodType, int range) // TODO: zrobić for'y zliczające zamiast zwracania pojedynczych wartości
        {
            if (neighborhoodType == NeighborhoodType.MIRROR_EXTENSION)
            {
                return (pixels, px, py, act) =>
                {
                    for (int x = px - range; x <= px + range; x++)
                    {
                        for (int y = py - range; y <= py + range; y++)
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
                            act(pixels[finalX, finalY]);
                        }
                    }
                };
            }
            else
            {
                return (pixels, px, py, act) =>
                {
                    for (int x = px - range; x <= px + range; x++)
                    {
                        for (int y = py - range; y <= py + range; y++)
                        {
                            if (x < 0 || x >= Width || y < 0 || y >= Height)
                            {
                                act(default);
                            }
                            else
                            {
                                act(pixels[x, y]);
                            }
                        }
                    }
                };
            }
        }

        private Action<Image, Action<T[,], int, int, Action<T>>, uint, uint, Image> GetFilterOperation(FilterType filter)
        {
            switch (filter)
            {
                //case FilterType.SUM: return Sum;
                case FilterType.AVERAGE: return Average;
                //case FilterType.MAXIMUM: return Maximum;
                //case FilterType.MINIMUM: return Minimum;
                //case FilterType.MEDIAN: return Median;
                //case FilterType.DIVERSITY: return Diversity;
                //case FilterType.RANGE: return Range;
                default: return Average;
            }
        }
    }
}
