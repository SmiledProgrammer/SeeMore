﻿using System;

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

        public override ImageGray<T> ToGray(GrayscaleConversionMethod method = GrayscaleConversionMethod.ARITHMETIC_MEAN)
        {
            ImageGray<T> grayImage = (ImageGray<T>)ImageFactory.Create<T>(Width, Height, ColorModel.GRAY);
            Func<double, double, double, double> conversionFunction;
            switch (method)
            {
                case GrayscaleConversionMethod.GEOMETRIC_MEAN:
                    conversionFunction = (r, g, b) => Math.Pow(r * g * b, 1.0 / 3.0);
                    break;
                case GrayscaleConversionMethod.HARMONIC_MEAN:
                    conversionFunction = (r, g, b) => 3.0 / (1.0 / r + 1.0 / g + 1.0 / b);
                    break;
                default:
                    conversionFunction = (r, g, b) => (r + g + b) / 3.0;
                    break;
            }
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grayImage.SetPixelValueFromDouble(x, y, conversionFunction(Convert.ToDouble(R.Pixels[x, y]), Convert.ToDouble(G.Pixels[x, y]), Convert.ToDouble(B.Pixels[x, y])));
                }
            }
            return grayImage;
        }

        protected override void Average(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageRGB<T> castedOriginalImage = (ImageRGB<T>)originalImage;
            ImageRGB<T> castedOutputImage = (ImageRGB<T>)outputImage;
            castedOutputImage.R.Average(castedOriginalImage.R, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.G.Average(castedOriginalImage.G, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.B.Average(castedOriginalImage.B, kernelFunction, x, y, neighborhoodSize);
        }

        protected override void Median(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageRGB<T> castedOriginalImage = (ImageRGB<T>)originalImage;
            ImageRGB<T> castedOutputImage = (ImageRGB<T>)outputImage;
            castedOutputImage.R.Median(castedOriginalImage.R, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.G.Median(castedOriginalImage.G, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.B.Median(castedOriginalImage.B, kernelFunction, x, y, neighborhoodSize);
        }

        protected override void Maximum(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageRGB<T> castedOriginalImage = (ImageRGB<T>)originalImage;
            ImageRGB<T> castedOutputImage = (ImageRGB<T>)outputImage;
            castedOutputImage.R.Maximum(castedOriginalImage.R, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.G.Maximum(castedOriginalImage.G, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.B.Maximum(castedOriginalImage.B, kernelFunction, x, y, neighborhoodSize);
        }

        protected override void Minimum(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageRGB<T> castedOriginalImage = (ImageRGB<T>)originalImage;
            ImageRGB<T> castedOutputImage = (ImageRGB<T>)outputImage;
            castedOutputImage.R.Minimum(castedOriginalImage.R, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.G.Minimum(castedOriginalImage.G, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.B.Minimum(castedOriginalImage.B, kernelFunction, x, y, neighborhoodSize);
        }

        protected override void Range(Image<T> originalImage, KernelFunction kernelFunction, uint neighborhoodSize, uint x, uint y, Image<T> outputImage)
        {
            ImageRGB<T> castedOriginalImage = (ImageRGB<T>)originalImage;
            ImageRGB<T> castedOutputImage = (ImageRGB<T>)outputImage;
            castedOutputImage.R.Range(castedOriginalImage.R, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.G.Range(castedOriginalImage.G, kernelFunction, x, y, neighborhoodSize);
            castedOutputImage.B.Range(castedOriginalImage.B, kernelFunction, x, y, neighborhoodSize);
        }

        public override ColorModel GetColorModel()
        {
            return ColorModel.RGB;
        }
    }
}
