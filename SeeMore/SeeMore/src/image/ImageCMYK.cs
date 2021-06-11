namespace SeeMore
{
    internal abstract class ImageCMYK<T> : GenericImage<T>
    {
        public Channel<T> C { get; set; }
        public Channel<T> M { get; set; }
        public Channel<T> Y { get; set; }
        public Channel<T> K { get; set; }

        protected ImageCMYK(uint width, uint height) : base(width, height)
        { }

        public override Image Clone()
        {
            ImageCMYK<T> clone = (ImageCMYK<T>)ImageFactory.Create(Width, Height, ColorModel.CMYK, GetDataType());
            clone.C = C.Clone();
            clone.M = M.Clone();
            clone.Y = Y.Clone();
            clone.K = K.Clone();
            return clone;
        }

        internal override GenericImage<T> Add(GenericImage<T> other)
        {
            ImageCMYK<T> outcome = (ImageCMYK<T>)ImageFactory.Create(Width, Height, GetColorModel(), GetDataType());
            ImageCMYK<T> otherCMYK = (ImageCMYK<T>)other.ToCMYK();
            outcome.C = C.Add(otherCMYK.C);
            outcome.M = M.Add(otherCMYK.M);
            outcome.Y = Y.Add(otherCMYK.Y);
            outcome.K = K.Add(otherCMYK.K);
            return outcome;
        }

        public override Image ToHSV()
        {
            return ToRGB().ToHSV();
        }

        public override Image ToCMYK()
        {
            return Clone();
        }

        public override Image ToGray(GrayscaleConversionMethod method = GrayscaleConversionMethod.ARITHMETIC_MEAN)
        {
            return ToRGB().ToGray(method);
        }

        internal override void Average(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageCMYK<T> castedOriginalImage = (ImageCMYK<T>)originalImage;
            ImageCMYK<T> castedOutputImage = (ImageCMYK<T>)outputImage;
            castedOutputImage.C.Average(castedOriginalImage.C, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.M.Average(castedOriginalImage.M, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.Y.Average(castedOriginalImage.Y, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.K.Average(castedOriginalImage.K, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Median(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageCMYK<T> castedOriginalImage = (ImageCMYK<T>)originalImage;
            ImageCMYK<T> castedOutputImage = (ImageCMYK<T>)outputImage;
            castedOutputImage.C.Median(castedOriginalImage.C, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.M.Median(castedOriginalImage.M, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.Y.Median(castedOriginalImage.Y, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.K.Median(castedOriginalImage.K, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Maximum(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageCMYK<T> castedOriginalImage = (ImageCMYK<T>)originalImage;
            ImageCMYK<T> castedOutputImage = (ImageCMYK<T>)outputImage;
            castedOutputImage.C.Maximum(castedOriginalImage.C, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.M.Maximum(castedOriginalImage.M, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.Y.Maximum(castedOriginalImage.Y, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.K.Maximum(castedOriginalImage.K, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Minimum(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageCMYK<T> castedOriginalImage = (ImageCMYK<T>)originalImage;
            ImageCMYK<T> castedOutputImage = (ImageCMYK<T>)outputImage;
            castedOutputImage.C.Minimum(castedOriginalImage.C, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.M.Minimum(castedOriginalImage.M, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.Y.Minimum(castedOriginalImage.Y, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.K.Minimum(castedOriginalImage.K, kernelFunction, x, y, neighborhoodSize);
        }

        internal override void Range(Image originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageCMYK<T> castedOriginalImage = (ImageCMYK<T>)originalImage;
            ImageCMYK<T> castedOutputImage = (ImageCMYK<T>)outputImage;
            castedOutputImage.C.Range(castedOriginalImage.C, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.M.Range(castedOriginalImage.M, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.Y.Range(castedOriginalImage.Y, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.K.Range(castedOriginalImage.K, kernelFunction, x, y, neighborhoodSize);
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.CMYK;
        }
    }
}
