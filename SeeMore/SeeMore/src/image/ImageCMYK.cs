using System;

namespace SeeMore
{
    public abstract class ImageCMYK<T> : Image<T>
    {
        public Channel<T> C { get; set; }
        public Channel<T> M { get; set; }
        public Channel<T> Y { get; set; }
        public Channel<T> K { get; set; }

        protected ImageCMYK(uint width, uint height) : base(width, height)
        { }

        public override Image<T> Clone()
        {
            ImageCMYK<T> clone = (ImageCMYK<T>)ImageFactory.Create<T>(Width, Height, ColorModel.CMYK);
            clone.C = C.Clone();
            clone.M = M.Clone();
            clone.Y = Y.Clone();
            clone.K = K.Clone();
            return clone;
        }

        public override ImageHSV<T> ToHSV()
        {
            return ToRGB().ToHSV();
        }

        public override ImageCMYK<T> ToCMYK()
        {
            return (ImageCMYK<T>)Clone();
        }

        public override ImageGray<T> ToGray(GrayscaleConversionMethod method = GrayscaleConversionMethod.ARITHMETIC_MEAN)
        {
            ImageGray<T> grayImage = (ImageGray<T>)ImageFactory.Create<T>(Width, Height, ColorModel.GRAY);
            Func<double, double, double, double, double> conversionFunction;
            switch (method)
            {
                case GrayscaleConversionMethod.GEOMETRIC_MEAN:
                    conversionFunction = (c, m, y, k) => Math.Pow(c * m * y * k, 1.0 / 4.0);
                    break;
                case GrayscaleConversionMethod.HARMONIC_MEAN:
                    conversionFunction = (c, m, y, k) => 4.0 / (1.0 / c + 1.0 / m + 1.0 / y + 1.0 / k);
                    break;
                default:
                    conversionFunction = (c, m, y, k) => (c + m + y + k) / 4.0;
                    break;
            }
            for (uint x = 0; x < Width; x++)
            {
                for (uint y = 0; y < Height; y++)
                {
                    grayImage.SetPixelValueFromDouble(x, y, conversionFunction(Convert.ToDouble(C.Pixels[x, y]), Convert.ToDouble(M.Pixels[x, y]), Convert.ToDouble(Y.Pixels[x, y]), Convert.ToDouble(K.Pixels[x, y])));
                }
            }
            return grayImage;
        }

        protected override void Average(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageCMYK<T> castedOriginalImage = (ImageCMYK<T>)originalImage;
            ImageCMYK<T> castedOutputImage = (ImageCMYK<T>)outputImage;
            castedOutputImage.C.Average(castedOriginalImage.C, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.M.Average(castedOriginalImage.M, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.Y.Average(castedOriginalImage.Y, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.K.Average(castedOriginalImage.K, neighborhoodFunction, x, y, neighborhoodSize);
        }

        protected override void Median(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageCMYK<T> castedOriginalImage = (ImageCMYK<T>)originalImage;
            ImageCMYK<T> castedOutputImage = (ImageCMYK<T>)outputImage;
            castedOutputImage.C.Median(castedOriginalImage.C, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.M.Median(castedOriginalImage.M, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.Y.Median(castedOriginalImage.Y, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.K.Median(castedOriginalImage.K, neighborhoodFunction, x, y, neighborhoodSize);
        }

        protected override void Maximum(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageCMYK<T> castedOriginalImage = (ImageCMYK<T>)originalImage;
            ImageCMYK<T> castedOutputImage = (ImageCMYK<T>)outputImage;
            castedOutputImage.C.Maximum(castedOriginalImage.C, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.M.Maximum(castedOriginalImage.M, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.Y.Maximum(castedOriginalImage.Y, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.K.Maximum(castedOriginalImage.K, neighborhoodFunction, x, y, neighborhoodSize);
        }

        protected override void Minimum(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageCMYK<T> castedOriginalImage = (ImageCMYK<T>)originalImage;
            ImageCMYK<T> castedOutputImage = (ImageCMYK<T>)outputImage;
            castedOutputImage.C.Minimum(castedOriginalImage.C, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.M.Minimum(castedOriginalImage.M, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.Y.Minimum(castedOriginalImage.Y, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.K.Minimum(castedOriginalImage.K, neighborhoodFunction, x, y, neighborhoodSize);
        }

        protected override void Range(Image<T> originalImage, NeighborhoodFunction neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageCMYK<T> castedOriginalImage = (ImageCMYK<T>)originalImage;
            ImageCMYK<T> castedOutputImage = (ImageCMYK<T>)outputImage;
            castedOutputImage.C.Range(castedOriginalImage.C, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.M.Range(castedOriginalImage.M, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.Y.Range(castedOriginalImage.Y, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.K.Range(castedOriginalImage.K, neighborhoodFunction, x, y, neighborhoodSize);
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.RGB;
        }
    }
}
