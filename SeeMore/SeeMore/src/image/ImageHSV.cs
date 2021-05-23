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
            // TODO
        }

        protected override void Median(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            // TODO
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.HSV;
        }
    }
}
