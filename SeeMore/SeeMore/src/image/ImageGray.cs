namespace SeeMore
{
    internal abstract class ImageGray<T> : GenericImage<T>
    {
        public Channel<T> Gray { get; set; }

        protected ImageGray(uint width, uint height) : base(width, height)
        { }

        public abstract void SetPixelValueFromDouble(int x, int y, double value);

        public override Image Clone()
        {
            ImageGray<T> clone = (ImageGray<T>)ImageFactory.Create(Width, Height, ColorModel.GRAY, GetDataType());
            clone.Gray = Gray.Clone();
            return clone;
        }

        internal override GenericImage<T> Add(GenericImage<T> other)
        {
            ImageGray<T> outcome = (ImageGray<T>)ImageFactory.Create(Width, Height, GetColorModel(), GetDataType());
            ImageGray<T> otherGray = (ImageGray<T>)other.ToGray();
            outcome.Gray = Gray.Add(otherGray.Gray);
            return outcome;
        }

        public override Image ToRGB()
        {
            ImageRGB<T> result = (ImageRGB<T>)ImageFactory.Create(Width, Height, ColorModel.RGB, GetDataType());
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

        public override Image ToHSV()
        {
            return ToRGB().ToHSV();
        }

        public override Image ToCMYK()
        {
            ImageCMYK<T> result = (ImageCMYK<T>)ImageFactory.Create(Width, Height, ColorModel.CMYK, GetDataType());
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

        public override Image ToGray(GrayscaleConversionMethod method = GrayscaleConversionMethod.ARITHMETIC_MEAN)
        {
            return Clone();
        }

        internal override void Average(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Average(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Median(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Median(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Maximum(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Maximum(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Minimum(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Minimum(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Range(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
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
