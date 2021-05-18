using ImageMagick;
using System.IO;

namespace SeeMore
{
    public static class ImageFactory
    {
        public static Image Create(uint width, uint height, DataType type, ColorModel model)
        {
            switch (model)
            {
                case ColorModel.RGB:
                    switch (type)
                    {
                        case DataType.UInt8:  return new ImageUInt8RGB(width, height);
                        case DataType.UInt16: return new ImageUInt8RGB(width, height); //TODO
                        case DataType.UInt32: return new ImageUInt8RGB(width, height); //TODO
                        case DataType.Double: return new ImageUInt8RGB(width, height); //TODO
                    }
                    break;
                case ColorModel.HSV:
                    //TODO
                    break;
                case ColorModel.CMYK:
                    //TODO
                    break;
            }
            return null;
        }

        public static Image LoadFromFile(string filepath)
        {
            var image = new MagickImage(filepath);
            ImageUInt8RGB result = new ImageUInt8RGB((uint)image.Width, (uint)image.Height);
            IPixelCollection<byte> pixelCollection = image.GetPixels();
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    IPixel<byte> pixel = pixelCollection.GetPixel(x, y);
                    result.R.Pixels[x, y] = pixel.GetChannel(0);
                    result.G.Pixels[x, y] = pixel.GetChannel(1);
                    result.B.Pixels[x, y] = pixel.GetChannel(2);
                }
            }
            return result;
        }

        public static void SaveImageToFile(string filepath, Image image)
        {
            ImageUInt8RGB rgbImage = image.ToByteRGBImage();
            byte[,] r = rgbImage.R.ToByteArray();
            byte[,] g = rgbImage.G.ToByteArray();
            byte[,] b = rgbImage.B.ToByteArray();
            byte[] data = new byte[image.Width * image.Height * 3];
            for (uint y = 0; y < image.Height; y++)
            {
                for (uint x = 0; x < image.Width; x++)
                {
                    data[3*image.Width*y + 3*x] = r[x, y];
                    data[3*image.Width*y + 3*x + 1] = g[x, y];
                    data[3*image.Width*y + 3*x + 2] = b[x, y];
                }
            }
            var memoryStream = new MemoryStream();
            var readSettings = new MagickReadSettings();
            readSettings.Width = (int)image.Width;
            readSettings.Height = (int)image.Height;
            readSettings.Format = MagickFormat.Rgb;
            var savedImage = new MagickImage(data, readSettings);
            savedImage.Format = MagickFormat.Png;
            savedImage.Write(filepath);
        }
    }
}
