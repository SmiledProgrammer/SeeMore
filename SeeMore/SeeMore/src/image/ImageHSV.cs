namespace SeeMore
{
    internal abstract class ImageHSV<T> : GenericImage<T>
    {
        public GenericChannel<T> H { get; protected set; }
        public GenericChannel<T> S { get; protected set; }
        public GenericChannel<T> V { get; protected set; }

        protected ImageHSV(uint width, uint height) : base(width, height)
        { }

        public override Image Clone()
        {
            ImageHSV<T> clone = (ImageHSV<T>)ImageFactory.Create(Width, Height, ColorModel.HSV, GetDataType());
            clone.H = (GenericChannel<T>)H.Clone();
            clone.S = (GenericChannel<T>)S.Clone();
            clone.V = (GenericChannel<T>)V.Clone();
            return clone;
        }

        internal override GenericImage<T> Add(GenericImage<T> other)
        {
            ImageHSV<T> outcome = (ImageHSV<T>)ImageFactory.Create(Width, Height, GetColorModel(), GetDataType());
            ImageHSV<T> otherHSV = (ImageHSV<T>)other.ToHSV();
            outcome.H = H.Add(otherHSV.H);
            outcome.S = S.Add(otherHSV.S);
            outcome.V = V.Add(otherHSV.V);
            return outcome;
        }

        public override Image ToHSV()
        {
            return Clone();
        }

        public override Image ToCMYK()
        {
            return ToRGB().ToCMYK();
        }

        public override Image ToGray(GrayscaleConversionMethod method = GrayscaleConversionMethod.ARITHMETIC_MEAN)
        {
            return ToRGB().ToGray(method);
        }

        internal override void Average(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageHSV<T> castedOriginalImage = (ImageHSV<T>)originalImage;
            ImageHSV<T> castedOutputImage = (ImageHSV<T>)outputImage;
            castedOutputImage.H.Average(castedOriginalImage.H, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.S.Average(castedOriginalImage.S, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.V.Average(castedOriginalImage.V, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Median(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageHSV<T> castedOriginalImage = (ImageHSV<T>)originalImage;
            ImageHSV<T> castedOutputImage = (ImageHSV<T>)outputImage;
            castedOutputImage.H.Median(castedOriginalImage.H, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.S.Median(castedOriginalImage.S, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.V.Median(castedOriginalImage.V, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Maximum(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageHSV<T> castedOriginalImage = (ImageHSV<T>)originalImage;
            ImageHSV<T> castedOutputImage = (ImageHSV<T>)outputImage;
            castedOutputImage.H.Maximum(castedOriginalImage.H, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.S.Maximum(castedOriginalImage.S, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.V.Maximum(castedOriginalImage.V, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Minimum(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageHSV<T> castedOriginalImage = (ImageHSV<T>)originalImage;
            ImageHSV<T> castedOutputImage = (ImageHSV<T>)outputImage;
            castedOutputImage.H.Minimum(castedOriginalImage.H, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.S.Minimum(castedOriginalImage.S, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.V.Minimum(castedOriginalImage.V, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Range(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageHSV<T> castedOriginalImage = (ImageHSV<T>)originalImage;
            ImageHSV<T> castedOutputImage = (ImageHSV<T>)outputImage;
            castedOutputImage.H.Range(castedOriginalImage.H, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.S.Range(castedOriginalImage.S, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.V.Range(castedOriginalImage.V, kernelFunction, x, y, neighborhoodSize);
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.HSV;
        }
    }
}
