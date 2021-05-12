using System;

namespace SeeMore
{
    public abstract class GenericImage<T> : Image
    {
        protected GenericImage(uint width, uint height) : base(width, height)
        { }

        public delegate void NeighborhoodFunction(T[,] pixels, int x, int y, Action<T> filterFunction);
        public delegate void FilterOperation(Image originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image outputImage);

        protected abstract void Average(Image originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image outputImage);
        //protected abstract void Maximum(Image result, uint x, uint y);
        //protected abstract void Minimum(Image result, uint x, uint y);
        protected abstract void Median(Image originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image outputImage);
        //protected abstract void Diversity(Image result, uint x, uint y);
        //protected abstract void Range(Image result, uint x, uint y);

        public override Image Filter(FilterType filter, NeighborhoodSize neighborhoodSize, NeighborhoodType neighborhoodType)
        {
            uint size = (uint)neighborhoodSize;
            uint range = size / 2;
            if (Width < range + 1 || Height < range + 1) // TODO: needs testing
            {
                throw new IndexOutOfRangeException("Neighborhood range cannot be greater than image size.");
            }

            Image result;
            if (neighborhoodType == NeighborhoodType.SKIP_UNDEFINED)
                result = Clone();
            else
                result = ImageFactory.Create(Width, Height, GetDataType(), GetColorModel());

            uint lowerX, upperX, lowerY, upperY;
            GetNeighborhoodArea(neighborhoodType, range, out lowerX, out upperX, out lowerY, out upperY);
            NeighborhoodFunction neighborhoodFunction = GetNeighborhoodFunction(neighborhoodType, (int)range);
            FilterOperation filterOperation = GetFilterOperation(filter);
            for (uint x = lowerX; x < upperX; x++)
            {
                for (uint y = lowerY; y < upperY; y++)
                {
                    filterOperation(this, neighborhoodFunction, size, x, y, result);
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

        private NeighborhoodFunction GetNeighborhoodFunction(NeighborhoodType neighborhoodType, int range) // TODO: zrobić for'y zliczające zamiast zwracania pojedynczych wartości
        {
            if (neighborhoodType == NeighborhoodType.MIRROR_EXTENSION)
            {
                return (pixels, px, py, action) =>
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
                            action(pixels[finalX, finalY]);
                        }
                    }
                };
            }
            else
            {
                return (pixels, px, py, action) =>
                {
                    for (int x = px - range; x <= px + range; x++)
                    {
                        for (int y = py - range; y <= py + range; y++)
                        {
                            if (x < 0 || x >= Width || y < 0 || y >= Height)
                                action(default);
                            else
                                action(pixels[x, y]);
                        }
                    }
                };
            }
        }

        private FilterOperation GetFilterOperation(FilterType filter)
        {
            switch (filter)
            {
                case FilterType.AVERAGE: return Average;
                //case FilterType.MAXIMUM: return Maximum;
                //case FilterType.MINIMUM: return Minimum;
                case FilterType.MEDIAN: return Median;
                //case FilterType.DIVERSITY: return Diversity;
                //case FilterType.RANGE: return Range;
                default: return Average;
            }
        }
    }
}
