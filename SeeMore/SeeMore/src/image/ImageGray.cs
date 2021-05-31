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

        public override ImageHSV<T> ToHSV()
        {
            return ToRGB().ToHSV();
        }

        public override ImageGray<T> ToGray(GrayscaleConversionMethod method = GrayscaleConversionMethod.ARITHMETIC_MEAN)
        {
            return (ImageGray<T>)Clone();
        }

        protected override void Average(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Average(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        protected override void Median(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Median(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        protected override void Maximum(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Maximum(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        protected override void Minimum(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Minimum(castedOriginalImage.Gray, kernelFunction, x, y, neighborhoodSize);
        }

        protected override void Range(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
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
