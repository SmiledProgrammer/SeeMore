using ImageMagick;
using System;

namespace SeeMore
{
    public static class ImageFactory
    {
        private static readonly string WrongTypeExceptionMessage = "Not supported image data type.";

        public static Image Create(uint width, uint height, ColorModel model = ColorModel.RGB, DataType dataType = DataType.UInt8)
        {
            switch (model)
            {
                case ColorModel.RGB:
                    switch (dataType)
                    {
                        case DataType.UInt8: return new ImageUInt8RGB(width, height);
                        case DataType.UInt16: return new ImageUInt16RGB(width, height);
                        case DataType.UInt32: return new ImageUInt32RGB(width, height);
                        case DataType.Double: return new ImageDoubleRGB(width, height);
                        default: throw new NotSupportedException(WrongTypeExceptionMessage);
                    }
                case ColorModel.HSV:
                    switch (dataType)
                    {
                        case DataType.UInt8: return new ImageUInt8HSV(width, height);
                        case DataType.UInt16: return new ImageUInt16HSV(width, height);
                        case DataType.UInt32: return new ImageUInt32HSV(width, height);
                        case DataType.Double: return new ImageDoubleHSV(width, height);
                        default: throw new NotSupportedException(WrongTypeExceptionMessage);
                    }
                case ColorModel.CMYK:
                    switch (dataType)
                    {
                        case DataType.UInt8: return new ImageUInt8CMYK(width, height);
                        case DataType.UInt16: return new ImageUInt16CMYK(width, height);
                        case DataType.UInt32: return new ImageUInt32CMYK(width, height);
                        case DataType.Double: return new ImageDoubleCMYK(width, height);
                        default: throw new NotSupportedException(WrongTypeExceptionMessage);
                    }
                case ColorModel.GRAY:
                    switch (dataType)
                    {
                        case DataType.UInt8: return new ImageUInt8Gray(width, height);
                        case DataType.UInt16: return new ImageUInt16Gray(width, height);
                        case DataType.UInt32: return new ImageUInt32Gray(width, height);
                        case DataType.Double: return new ImageDoubleGray(width, height);
                        default: throw new NotSupportedException(WrongTypeExceptionMessage);
                    }
            }
            throw new NotSupportedException(WrongTypeExceptionMessage);
        }

        public static Image LoadFromFile(string filepath)
        {
            var image = new MagickImage(filepath);
            IPixelCollection<byte> pixelCollection = image.GetPixels();
            ImageUInt8RGB result = new ImageUInt8RGB((uint)image.Width, (uint)image.Height);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    IPixel<byte> pixel = pixelCollection.GetPixel(x, y);
                    result.R[x, y] = pixel.GetChannel(0);
                    result.G[x, y] = pixel.GetChannel(1);
                    result.B[x, y] = pixel.GetChannel(2);
                }
            }
            return result;
        }

        public static void SaveImageToFile(string filepath, Image image, MagickFormat format = MagickFormat.Png)
        {
            ImageUInt8RGB byteRgbImage = (ImageUInt8RGB)image.ToRGB().ToUInt8();
            uint width = byteRgbImage.Width;
            uint height = byteRgbImage.Height;
            byte[,] r = byteRgbImage.R.ToByteArray();
            byte[,] g = byteRgbImage.G.ToByteArray();
            byte[,] b = byteRgbImage.B.ToByteArray();
            byte[] data = new byte[width * height * 3];
            for (uint y = 0; y < height; y++)
            {
                for (uint x = 0; x < width; x++)
                {
                    data[3*width*y + 3*x] = r[x, y];
                    data[3*width*y + 3*x + 1] = g[x, y];
                    data[3*width*y + 3*x + 2] = b[x, y];
                }
            }
            var readSettings = new MagickReadSettings
            {
                Width = (int)width,
                Height = (int)height,
                Format = MagickFormat.Rgb
            };
            var savedImage = new MagickImage(data, readSettings);
            savedImage.Format = format;
            savedImage.Write(filepath);
        }
    }
}
