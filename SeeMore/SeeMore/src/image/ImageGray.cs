namespace SeeMore
{
    public abstract class ImageGray<T> : GenericImage<T>
    {
        public Channel<T> Gray { get; set; }

        protected ImageGray(uint width, uint height) : base(width, height)
        { }

        public override Image Clone()
        {
            ImageGray<T> clone = (ImageGray<T>)ImageFactory.Create(Width, Height, GetDataType(), ColorModel.GRAY);
            clone.Gray = Gray.Clone();
            return clone;
        }

        protected override void Average(Image originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Average(castedOriginalImage.Gray, neighborhoodFunction, x, y);
        }

        protected override void Median(Image originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageGray<T> castedOriginalImage = (ImageGray<T>)originalImage;
            ImageGray<T> castedOutputImage = (ImageGray<T>)outputImage;
            castedOutputImage.Gray.Median(castedOriginalImage.Gray, neighborhoodFunction, x, y, neighborhoodSize);
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.GRAY;
        }
    }
}
