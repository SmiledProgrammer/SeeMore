using System;

namespace SeeMore
{
    public abstract class Image<T>
    {
        public uint Width { get; }
        public uint Height { get; }

        protected Image(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public delegate void NeighborhoodFunction(T[,] pixels, uint x, uint y, Action<T> filterFunction);
        public delegate void FilterOperation(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage);

        public abstract Image<T> Clone();
        public abstract ImageUInt8RGB ToByteRGBImage();
        public abstract ImageRGB<T> ToRGB();
        public abstract ImageHSV<T> ToHSV();
        public abstract DataType GetDataType();
        public abstract ColorModel GetColorModel();

        protected abstract void Average(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage);
        //protected abstract void Maximum(Image result, uint x, uint y);
        //protected abstract void Minimum(Image result, uint x, uint y);
        protected abstract void Median(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage);
        //protected abstract void Range(Image result, uint x, uint y);

        public Image<T> Filter(FilterType filter, NeighborhoodSize neighborhoodSize, EdgeHandling edgeHandling)
        {
            uint size = (uint)neighborhoodSize;
            uint range = size / 2;
            if (Width < range + 1 || Height < range + 1) // TODO: needs testing
            {
                throw new IndexOutOfRangeException("Neighborhood range cannot be greater than image size.");
            }

            Image<T> result;
            if (edgeHandling == EdgeHandling.SKIP_UNDEFINED)
                result = Clone();
            else
                result = ImageFactory.Create<T>(Width, Height, GetColorModel());

            uint lowerX, upperX, lowerY, upperY;
            GetNeighborhoodArea(edgeHandling, range, out lowerX, out upperX, out lowerY, out upperY);
            NeighborhoodFunction neighborhoodFunction = GetNeighborhoodFunction(edgeHandling, (int)range);
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

        private void GetNeighborhoodArea(EdgeHandling edgeHandling, uint neighborhoodRange, out uint lowerX, out uint upperX, out uint lowerY, out uint upperY)
        {
            if (edgeHandling == EdgeHandling.SKIP_UNDEFINED)
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

        private NeighborhoodFunction GetNeighborhoodFunction(EdgeHandling edgeHandling, int range)
        {
            if (edgeHandling == EdgeHandling.MIRROR_EXTENSION)
            {
                return (pixels, ppx, ppy, action) =>
                {
                    int px = (int)ppx;
                    int py = (int)ppy;
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
                return (pixels, ppx, ppy, action) =>
                {
                    int px = (int)ppx;
                    int py = (int)ppy;
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
