namespace SeeMore
{
    public class ImageUInt8HSV : ImageHSV<byte>
    {
        public ImageUInt8HSV(uint width, uint height) : base(width, height)
        {
            H = new ChannelUInt8(width, height);
            S = new ChannelUInt8(width, height);
            V = new ChannelUInt8(width, height);
        }

        public override ImageRGB<byte> ToRGB()
        {
            ImageRGB<byte> rgbImage = new ImageUInt8RGB(Width, Height);
            for (uint x = 0; x < Width; x++)
            {
                for (uint y = 0; y < Height; y++)
                {
                    byte h = H.Pixels[x, y];
                    byte s = S.Pixels[x, y];
                    byte v = V.Pixels[x, y];
                    if (s == 0)
                    {
                        rgbImage.R.Pixels[x, y] = v;
                        rgbImage.G.Pixels[x, y] = v;
                        rgbImage.B.Pixels[x, y] = v;
                        break;
                    }
                    byte region = (byte)(h / 43);
                    byte remainder = (byte)((h - (region * 43)) * 6);
                    byte p = (byte)((v * (255 - s)) >> 8);
                    byte q = (byte)((v * (255 - ((s * remainder) >> 8))) >> 8);
                    byte t = (byte)((v * (255 - ((s * (255 - remainder)) >> 8))) >> 8);
                    switch (region)
                    {
                        case 0:
                            rgbImage.R.Pixels[x, y] = v;
                            rgbImage.G.Pixels[x, y] = t;
                            rgbImage.B.Pixels[x, y] = p;
                            break;
                        case 1:
                            rgbImage.R.Pixels[x, y] = q;
                            rgbImage.G.Pixels[x, y] = v;
                            rgbImage.B.Pixels[x, y] = p;
                            break;
                        case 2:
                            rgbImage.R.Pixels[x, y] = p;
                            rgbImage.G.Pixels[x, y] = v;
                            rgbImage.B.Pixels[x, y] = t;
                            break;
                        case 3:
                            rgbImage.R.Pixels[x, y] = p;
                            rgbImage.G.Pixels[x, y] = q;
                            rgbImage.B.Pixels[x, y] = v;
                            break;
                        case 4:
                            rgbImage.R.Pixels[x, y] = t;
                            rgbImage.G.Pixels[x, y] = p;
                            rgbImage.B.Pixels[x, y] = v;
                            break;
                        default:
                            rgbImage.R.Pixels[x, y] = v;
                            rgbImage.G.Pixels[x, y] = p;
                            rgbImage.B.Pixels[x, y] = q;
                            break;
                    }
                }
            }
            return rgbImage;
        }

        public override ImageUInt8RGB ToByteRGBImage()
        {
            return (ImageUInt8RGB)ToRGB();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt8;
        }
    }
}
