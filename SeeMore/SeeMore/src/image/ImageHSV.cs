namespace SeeMore
{
    public abstract class ImageHSV<T> : GenericImage<T> // TODO: remove "public"
    {
        public Channel<T> H { get; protected set; }
        public Channel<T> S { get; protected set; }
        public Channel<T> V { get; protected set; }

        protected ImageHSV(uint width, uint height) : base(width, height)
        { }

        public override Image Clone()
        {
            ImageHSV<T> clone = (ImageHSV<T>)ImageFactory.Create(Width, Height, GetDataType(), ColorModel.HSV);
            clone.H = H.Clone();
            clone.S = S.Clone();
            clone.V = V.Clone();
            return clone;
        }

        protected override void Average(Image originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            // TODO
        }

        protected override void Median(Image originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            // TODO
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.HSV;
        }

        public abstract ImageRGB<T> ToRGB();
    }
}
