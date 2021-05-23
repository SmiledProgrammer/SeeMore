using ImageMagick;
using System;

namespace SeeMore
{
    public static class ImageFactory
    {
        public static Image<T> Create<T>(uint width, uint height, ColorModel model = ColorModel.RGB)
        {
            string type = typeof(T).FullName;
            switch (model)
            {
                case ColorModel.RGB:
                    switch (type)
                    {
                        case "System.Byte":  return new ImageUInt8RGB(width, height) as Image<T>;
                        case "System.Int16": return null; //TODO
                        case "System.Int32": return null; //TODO
                        case "System.Double": return null; //TODO
                        default: throw new NotSupportedException("Neighborhood range cannot be greater than image size.");
                    }
                case ColorModel.HSV:
                    switch (type)
                    {
                        case "System.Byte": return new ImageUInt8HSV(width, height) as Image<T>;
                        case "System.Int16": return null; //TODO
                        case "System.Int32": return null; //TODO
                        case "System.Double": return null; //TODO
                        default: throw new NotSupportedException("Neighborhood range cannot be greater than image size.");
                    }
                case ColorModel.CMYK:
                    switch (type)
                    {
                        case "System.Byte": return null;
                        case "System.Int16": return null; //TODO
                        case "System.Int32": return null; //TODO
                        case "System.Double": return null; //TODO
                        default: throw new NotSupportedException("Neighborhood range cannot be greater than image size.");
                    }
                case ColorModel.GRAY:
                    switch (type)
                    {
                        case "System.Byte": return new ImageUInt8Gray(width, height) as Image<T>;
                        case "System.Int16": return null; //TODO
                        case "System.Int32": return null; //TODO
                        case "System.Double": return null; //TODO
                        default: throw new NotSupportedException("Neighborhood range cannot be greater than image size.");
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
            var readSettings = new MagickReadSettings();
            readSettings.Width = (int)width;
            readSettings.Height = (int)height;
            readSettings.Format = MagickFormat.Rgb;
            var savedImage = new MagickImage(data, readSettings);
            savedImage.Format = MagickFormat.Png;
            savedImage.Write(filepath);
        }
    }
}
