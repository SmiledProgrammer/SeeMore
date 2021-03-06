using System;

namespace SeeMore
{
    internal abstract class GenericImage<T> : Image
    {
        protected GenericImage(uint width, uint height) : base(width, height)
        { }

        internal delegate void KernelFunction(GenericChannel<T> channel, uint x, uint y, Action<double> filterFunction);
        internal delegate void FilterOperation(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage);

        internal abstract GenericImage<T> Add(GenericImage<T> other);

        internal abstract void Average(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage);
        internal abstract void Median(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage);
        internal abstract void Maximum(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage);
        internal abstract void Minimum(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage);
        internal abstract void Range(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage);

        public override Image Filter(Kernel kernel, FilterType filter = FilterType.AVERAGE, EdgeHandling edgeHandling = EdgeHandling.MIRROR_EXTENSION)
        {
            uint size = kernel.Size;
            uint range = size / 2;
            if (Width < range + 1 || Height < range + 1)
            {
                throw new IndexOutOfRangeException("Neighborhood range cannot be greater than image size.");
            }

            Image result;
            if (edgeHandling == EdgeHandling.SKIP_UNDEFINED)
                result = Clone();
            else
                result = ImageFactory.Create(Width, Height, GetColorModel(), GetDataType());

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

        public override Image Sobel()
        {
            Image input = Clone().ToGray();
            GenericImage<T> verticalSobel = (GenericImage<T>)input.Filter(KernelFactory.SobelVertical(), FilterType.AVERAGE);
            GenericImage<T> horizontalSobel = (GenericImage <T>)input.Filter(KernelFactory.SobelHorizontal(), FilterType.AVERAGE);
            GenericImage<T> outcome = verticalSobel.Add(horizontalSobel);
            return outcome;
        }

        public override Channel GetChannel(ChannelType type)
        {
            int channelCode = (int)type % 10;
            int desiredColorModelCode = (int)type / 10;
            int currentColorModelCode = (int)GetColorModel();
            if (desiredColorModelCode == currentColorModelCode)
            {
                switch (GetColorModel())
                {
                    case ColorModel.RGB:
                        switch (channelCode)
                        {
                            case 0: return ((ImageRGB<T>)this).R;
                            case 1: return ((ImageRGB<T>)this).G;
                            case 2: return ((ImageRGB<T>)this).B;
                            default: throw new InvalidOperationException();
                        }
                    case ColorModel.HSV:
                        switch (channelCode)
                        {
                            case 0: return ((ImageHSV<T>)this).H;
                            case 1: return ((ImageHSV<T>)this).S;
                            case 2: return ((ImageHSV<T>)this).V;
                            default: throw new InvalidOperationException();
                        }
                    case ColorModel.CMYK:
                        switch (channelCode)
                        {
                            case 0: return ((ImageCMYK<T>)this).C;
                            case 1: return ((ImageCMYK<T>)this).M;
                            case 2: return ((ImageCMYK<T>)this).Y;
                            case 3: return ((ImageCMYK<T>)this).K;
                            default: throw new InvalidOperationException();
                        }
                    case ColorModel.GRAY:
                        if (channelCode == 0)
                            return ((ImageGray<T>)this).Gray;
                        else
                            throw new InvalidOperationException();
                }
                throw new InvalidOperationException();
            }
            else
            {
                string channelName = type.ToString("g");
                string currentColorModelName = GetColorModel().ToString("g");
                string desiredColorModelName = ((ColorModel)desiredColorModelCode).ToString("g");
                throw new InvalidOperationException(
                    $"Cannot get desired channel \"{channelName}\" because color model of the image is {currentColorModelName}.\n" +
                    $"Convert the image to {desiredColorModelName} first, using: To{desiredColorModelName}()."
                    );
            }
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
                case FilterType.MEDIAN: return Median;
                case FilterType.MAXIMUM: return Maximum;
                case FilterType.MINIMUM: return Minimum;
                case FilterType.RANGE: return Range;
                default: return Average;
            }
        }
    }
}
