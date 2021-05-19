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
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    byte h = H[x, y];
                    byte s = S[x, y];
                    byte v = V[x, y];
                    if (s == 0)
                    {
                        rgbImage.R[x, y] = v;
                        rgbImage.G[x, y] = v;
                        rgbImage.B[x, y] = v;
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
                            rgbImage.R[x, y] = v;
                            rgbImage.G[x, y] = t;
                            rgbImage.B[x, y] = p;
                            break;
                        case 1:
                            rgbImage.R[x, y] = q;
                            rgbImage.G[x, y] = v;
                            rgbImage.B[x, y] = p;
                            break;
                        case 2:
                            rgbImage.R[x, y] = p;
                            rgbImage.G[x, y] = v;
                            rgbImage.B[x, y] = t;
                            break;
                        case 3:
                            rgbImage.R[x, y] = p;
                            rgbImage.G[x, y] = q;
                            rgbImage.B[x, y] = v;
                            break;
                        case 4:
                            rgbImage.R[x, y] = t;
                            rgbImage.G[x, y] = p;
                            rgbImage.B[x, y] = v;
                            break;
                        default:
                            rgbImage.R[x, y] = v;
                            rgbImage.G[x, y] = p;
                            rgbImage.B[x, y] = q;
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
