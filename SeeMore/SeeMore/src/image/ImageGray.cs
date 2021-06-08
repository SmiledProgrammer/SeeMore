namespace SeeMore
{
    public abstract class ImageGray<T> : Image<T>
    {
        public Channel<T> Gray { get; set; }

        protected ImageGray(uint width, uint height) : base(width, height)
        { }

        public abstract void SetPixelValueFromDouble(int x, int y, double value);

        public override Image<T> Clone()
        {
            ImageGray<T> clone = (ImageGray<T>)ImageFactory.Create<T>(Width, Height, ColorModel.GRAY);
            clone.Gray = Gray.Clone();
            return clone;
        }

        public override Image<T> Add(Image<T> other)
        {
            ImageGray<T> outcome = (ImageGray<T>)ImageFactory.Create<T>(Width, Height, GetColorModel());
            ImageGray<T> otherGray = other.ToGray();
            outcome.Gray = Gray.Add(otherGray.Gray);
            return outcome;
        }

        public override ImageRGB<T> ToRGB()
        {
            ImageRGB<T> result = (ImageRGB<T>)ImageFactory.Create<T>(Width, Height, ColorModel.RGB);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    result.R[x, y] = Gray[x, y];
                    result.G[x, y] = Gray[x, y];
                    result.B[x, y] = Gray[x, y];
                }
            }
            return result;
        }

        public override ImageHSV<T> ToHSV()
        {
            return ToRGB().ToHSV();
        }

        public override ImageCMYK<T> ToCMYK()
        {
            ImageCMYK<T> result = (ImageCMYK<T>)ImageFactory.Create<T>(Width, Height, ColorModel.CMYK);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    result.C[x, y] = Gray[x, y];
                    result.M[x, y] = Gray[x, y];
                    result.Y[x, y] = Gray[x, y];
                    result.K[x, y] = Gray[x, y];
                }
            }
            return result;
        }

        public override ImageGray<T> ToGray(GrayscaleConversionMethod method = GrayscaleConversionMethod.ARITHMETIC_MEAN)
        {
            return (ImageGray<T>)Clone();
        }

        internal override void Average(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Average(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Median(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Median(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Maximum(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Maximum(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Minimum(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Minimum(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Range(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Range(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.GRAY;
        }
    }
}
