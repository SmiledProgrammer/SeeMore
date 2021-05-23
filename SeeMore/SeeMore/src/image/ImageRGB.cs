namespace SeeMore
{
    public abstract class ImageRGB<T> : Image<T>
    {
        public Channel<T> R { get; set; }
        public Channel<T> G { get; set; }
        public Channel<T> B { get; set; }

        protected ImageRGB(uint width, uint height) : base(width, height)
        { }

        public override Image<T> Clone()
        {
            ImageRGB<T> clone = (ImageRGB<T>)ImageFactory.Create<T>(Width, Height, ColorModel.RGB);
            clone.R = R.Clone();
            clone.G = G.Clone();
            clone.B = B.Clone();
            return clone;
        }

        public override ImageRGB<T> ToRGB()
        {
            return (ImageRGB<T>)Clone();
        }

        protected override void Average(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageRGB<T> castedOriginalImage = (ImageRGB<T>)originalImage;
            ImageRGB<T> castedOutputImage = (ImageRGB<T>)outputImage;
            castedOutputImage.R.Average(castedOriginalImage.R, neighborhoodFunction, x, y);
            castedOutputImage.G.Average(castedOriginalImage.G, neighborhoodFunction, x, y);
            castedOutputImage.B.Average(castedOriginalImage.B, neighborhoodFunction, x, y);
        }

        protected override void Median(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageRGB<T> castedOriginalImage = (ImageRGB<T>)originalImage;
            ImageRGB<T> castedOutputImage = (ImageRGB<T>)outputImage;
            castedOutputImage.R.Median(castedOriginalImage.R, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.G.Median(castedOriginalImage.G, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.B.Median(castedOriginalImage.B, neighborhoodFunction, x, y, neighborhoodSize);
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.RGB;
        }
    }
}
