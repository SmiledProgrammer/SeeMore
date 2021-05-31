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

        public delegate void KernelFunction(Channel<T> channel, uint x, uint y, Action<double> filterFunction);
        public delegate void FilterOperation(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage);

        public abstract Image<T> Clone();
        public abstract ImageRGB<T> ToRGB();
        public abstract ImageHSV<T> ToHSV();
        public abstract ImageCMYK<T> ToCMYK();
        public abstract ImageGray<T> ToGray(GrayscaleConversionMethod method = GrayscaleConversionMethod.ARITHMETIC_MEAN);
        public abstract ImageUInt8RGB ToByteRGBImage();
        public abstract DataType GetDataType();
        public abstract ColorModel GetColorModel();

        protected abstract void Average(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage);
        protected abstract void Median(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage);
        protected abstract void Maximum(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage);
        protected abstract void Minimum(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage);
        protected abstract void Range(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage);

        public Image<T> Filter(FilterType filter, Kernel kernel, EdgeHandling edgeHandling)
        {
            uint size = kernel.Size;
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
            GetFilterArea(edgeHandling, range, out lowerX, out upperX, out lowerY, out upperY);
            KernelFunction kernelFunction = GetKernelFunction(kernel, edgeHandling, (int)range);
            FilterOperation filterOperation = GetFilterOperation(filter);
            for (uint x = lowerX; x < upperX; x++)
            {
                for (uint y = lowerY; y < upperY; y++)
                {
                    filterOperation(this, kernelFunction, size, x, y, result);
                }
            }
            return result;
        }

        private void GetFilterArea(EdgeHandling edgeHandling, uint range, out uint lowerX, out uint upperX, out uint lowerY, out uint upperY)
        {
            if (edgeHandling == EdgeHandling.SKIP_UNDEFINED)
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
        }

        private KernelFunction GetKernelFunction(Kernel kernel, EdgeHandling edgeHandling, int range)
        {
            if (edgeHandling == EdgeHandling.MIRROR_EXTENSION)
            {
                return (channel, ppx, ppy, action) =>
                {
                    int middleX = (int)ppx;
                    int middleY = (int)ppy;
                    for (int x = 0; x < kernel.Size; x++)
                    {
                        for (int y = 0; y < kernel.Size; y++)
                        {
                            int pixelX = middleX - range + x;
                            int pixelY = middleY - range + y;
                            int finalX, finalY;
                            if (pixelX < 0)
                                finalX = -pixelX;
                            else if (pixelX >= Width)
                                finalX = (int)(Width - (pixelX - Width + 1));
                            else
                                finalX = pixelX;
                            if (pixelY < 0)
                                finalY = -pixelY;
                            else if (pixelY >= Height)
                                finalY = (int)(Height - (pixelY - Height + 1));
                            else
                                finalY = pixelY;
                            action(channel.GetMultipliedValue((uint)finalX, (uint)finalY, kernel[(uint)x, (uint)y]));
                        }
                    }
                };
            }
            else
            {
                return (channel, ppx, ppy, action) =>
                {
                    int middleX = (int)ppx;
                    int middleY = (int)ppy;
                    for (int x = 0; x < kernel.Size; x++)
                    {
                        for (int y = 0; y < kernel.Size; y++)
                        {
                            int pixelX = middleX - range + x;
                            int pixelY = middleY - range + y;
                            if (pixelX < 0 || pixelX >= Width || pixelY < 0 || pixelY >= Height)
                                action(default);
                            else
                                action(channel.GetMultipliedValue((uint)pixelX, (uint)pixelY, kernel[(uint)x, (uint)y]));
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
                case FilterType.MEDIAN:  return Median;
                case FilterType.MAXIMUM: return Maximum;
                case FilterType.MINIMUM: return Minimum;
                case FilterType.RANGE:   return Range;
                default: return Average;
            }
        }
    }
}
