namespace SeeMore
{
    public abstract class ImageHSV<T> : Image<T>
    {
        public Channel<T> H { get; protected set; }
        public Channel<T> S { get; protected set; }
        public Channel<T> V { get; protected set; }

        protected ImageHSV(uint width, uint height) : base(width, height)
        { }

        public override Image<T> Clone()
        {
            ImageHSV<T> clone = (ImageHSV<T>)ImageFactory.Create<T>(Width, Height, ColorModel.HSV);
            clone.H = H.Clone();
            clone.S = S.Clone();
            clone.V = V.Clone();
            return clone;
        }

        public override ImageHSV<T> ToHSV()
        {
            return (ImageHSV<T>)Clone();
        }

        protected override void Average(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageHSV<T> castedOriginalImage = (ImageHSV<T>)originalImage;
            ImageHSV<T> castedOutputImage = (ImageHSV<T>)outputImage;
            castedOutputImage.H.Average(castedOriginalImage.H, neighborhoodFunction, x, y);
            castedOutputImage.S.Average(castedOriginalImage.S, neighborhoodFunction, x, y);
            castedOutputImage.V.Average(castedOriginalImage.V, neighborhoodFunction, x, y);
        }

        protected override void Median(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageHSV<T> castedOriginalImage = (ImageHSV<T>)originalImage;
            ImageHSV<T> castedOutputImage = (ImageHSV<T>)outputImage;
            castedOutputImage.H.Median(castedOriginalImage.H, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.S.Median(castedOriginalImage.S, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.V.Median(castedOriginalImage.V, neighborhoodFunction, x, y, neighborhoodSize);
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.HSV;
        }
    }
}
