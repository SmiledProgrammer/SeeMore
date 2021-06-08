using ImageMagick;
using System;

namespace SeeMore
{
    public static class ImageFactory
    {
        private static string WrongTypeExceptionMessage = "Neighborhood range cannot be greater than image size.";

        public static Image<T> Create<T>(uint width, uint height, ColorModel model = ColorModel.RGB)
        {
            string type = typeof(T).FullName;
            switch (model)
            {
                case ColorModel.RGB:
                    switch (type)
                    {
                        case "System.Byte":  return new ImageUInt8RGB(width, height) as Image<T>;
                        case "System.Int16": return new ImageUInt16RGB(width, height) as Image<T>;
                        case "System.Int32": return null;
                        case "System.Double": return null;
                        default: throw new NotSupportedException(WrongTypeExceptionMessage);
                    }
                case ColorModel.HSV:
                    switch (type)
                    {
                        case "System.Byte": return new ImageUInt8HSV(width, height) as Image<T>;
                        case "System.Int16": return new ImageUInt16HSV(width, height) as Image<T>;
                        case "System.Int32": return null; //TODO
                        case "System.Double": return null; //TODO
                        default: throw new NotSupportedException(WrongTypeExceptionMessage);
                    }
                case ColorModel.CMYK:
                    switch (type)
                    {
                        case "System.Byte": return new ImageUInt8CMYK(width, height) as Image<T>;
                        case "System.Int16": return new ImageUInt16CMYK(width, height) as Image<T>;
                        case "System.Int32": return null; //TODO
                        case "System.Double": return null; //TODO
                        default: throw new NotSupportedException(WrongTypeExceptionMessage);
                    }
                case ColorModel.GRAY:
                    switch (type)
                    {
                        case "System.Byte": return new ImageUInt8Gray(width, height) as Image<T>;
                        case "System.Int16": return new ImageUInt16Gray(width, height) as Image<T>;
                        case "System.Int32": return null; //TODO
                        case "System.Double": return null; //TODO
                        default: throw new NotSupportedException(WrongTypeExceptionMessage);
                    }
            }
            return null;
        }

        public static ImageUInt8RGB LoadFromFile(string filepath)
        {
            var image = new MagickImage(filepath);
            ImageUInt8RGB result = new ImageUInt8RGB((uint)image.Width, (uint)image.Height);
            IPixelCollection<byte> pixelCollection = image.GetPixels();
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

        public static void SaveImageToFile(string filepath, ImageUInt8RGB byteRgbImage, MagickFormat format = MagickFormat.Png)
        {
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
