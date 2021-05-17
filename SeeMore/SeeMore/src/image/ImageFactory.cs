using ImageMagick;
using System; // TODO: remove l8r

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
            using (var image = new MagickImage(filepath))
            {
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
        }

        public static void SaveImageToFile(string filepath, Image image) // TODO
        {
            //byte[] data = new byte
            //var image = new MagickImage(data);
        }
    }
}
