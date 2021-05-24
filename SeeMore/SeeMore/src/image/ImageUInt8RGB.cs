using System;

namespace SeeMore
{
    public class ImageUInt8RGB : ImageRGB<byte>
    {
        public ImageUInt8RGB(uint width, uint height) : base(width, height)
        {
            R = new ChannelUInt8(width, height);
            G = new ChannelUInt8(width, height);
            B = new ChannelUInt8(width, height);
        }

        public override ImageHSV<byte> ToHSV()
        {
            ImageHSV<byte> hsvImage = new ImageUInt8HSV(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    byte r = R[x, y];
                    byte g = G[x, y];
                    byte b = B[x, y];
                    byte min = Math.Min(r, Math.Min(g, b));
                    byte max = Math.Max(r, Math.Max(g, b));
                    hsvImage.V[x, y] = max;
                    if (max == 0)
                    {
                        hsvImage.S[x, y] = 0;
                        hsvImage.H[x, y] = 0;
                        continue;
                    }
                    byte diff = (byte)(max - min);
                    byte saturation = (byte)(255 * (long)diff / max);
                    hsvImage.S[x, y] = saturation;
                    if (saturation == 0)
                    {
                        hsvImage.H[x, y] = 0;
                        continue;
                    }
                    if (max == r)
                        hsvImage.H[x, y] = (byte)(43 * (g - b) / diff);
                    else if (max == g)
                        hsvImage.H[x, y] = (byte)(85 + 43 * (b - r) / diff);
                    else
                        hsvImage.H[x, y] = (byte)(171 + 43 * (r - g) / diff);
                }
            }
            return hsvImage;
        }

        public override ImageGray<byte> ToGray(GrayscaleConversionMethod method = GrayscaleConversionMethod.ARITHMETIC_MEAN) // TODO: try to move this function to ImageRGB
        {
            ImageUInt8Gray grayImage = new ImageUInt8Gray(Width, Height);
            Func<int, int, int, double> conversionFunction;
            switch (method)
            {
                case GrayscaleConversionMethod.GEOMETRIC_MEAN:
                    conversionFunction = (r, g, b) => (double)Math.Pow(r * g * b, 1.0 / 3.0);
                    break;
                case GrayscaleConversionMethod.HARMONIC_MEAN:
                    conversionFunction = (r, g, b) => (double)(3.0 / (1.0 / r + 1.0 / g + 1.0 / b));
                    break;
                default:
                    conversionFunction = (r, g, b) => (r + g + b) / 3.0;
                    break;
            }
            for (uint x = 0; x < Width; x++)
            {
                for (uint y = 0; y < Height; y++)
                {
                    grayImage.Gray.Pixels[x, y] = (byte)conversionFunction(R.Pixels[x, y], G.Pixels[x, y], B.Pixels[x, y]);
                }
            }
            return grayImage;
        }

        public override ImageUInt8RGB ToByteRGBImage()
        {
            return (ImageUInt8RGB)Clone();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt8;
        }
    }
}
