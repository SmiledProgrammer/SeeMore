namespace SeeMore
{
    public class ImageUInt16HSV : ImageHSV<ushort>
    {
        public ImageUInt16HSV(uint width, uint height) : base(width, height)
        {
            H = new ChannelUInt16(width, height);
            S = new ChannelUInt16(width, height);
            V = new ChannelUInt16(width, height);
        }

        public override ImageRGB<ushort> ToRGB()
        {
            ImageRGB<ushort> rgbImage = new ImageUInt16RGB(Width, Height);
            int multiplier = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ushort h = H[x, y];
                    ushort s = S[x, y];
                    ushort v = V[x, y];
                    if (s == 0)
                    {
                        rgbImage.R[x, y] = v;
                        rgbImage.G[x, y] = v;
                        rgbImage.B[x, y] = v;
                        continue;
                    }
                    ushort region = (ushort)(h / (43 * multiplier));
                    ushort remainder = (ushort)((h - (region * 43* multiplier)) * 6);
                    ushort p = (ushort)((v * (ushort.MaxValue - s)) >> 16);
                    ushort q = (ushort)((v * (ushort.MaxValue - ((s * remainder) >> 16))) >> 16);
                    ushort t = (ushort)((v * (ushort.MaxValue - ((s * (ushort.MaxValue - remainder)) >> 16))) >> 16);
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
            return ToRGB().ToByteRGBImage();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt16;
        }
    }
}
