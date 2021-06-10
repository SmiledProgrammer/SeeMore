namespace SeeMore
{
    public class ImageDoubleHSV : ImageHSV<double>
    {
        public ImageDoubleHSV(uint width, uint height) : base(width, height)
        {
            H = new ChannelDouble(width, height);
            S = new ChannelDouble(width, height);
            V = new ChannelDouble(width, height);
        }

        public override ImageRGB<double> ToRGB()
        {
            ImageRGB<double> rgbImage = new ImageDoubleRGB(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    double h = H[x, y];
                    double s = S[x, y];
                    double v = V[x, y];
                    if (s == 0)
                    {
                        rgbImage.R[x, y] = v;
                        rgbImage.G[x, y] = v;
                        rgbImage.B[x, y] = v;
                        continue;
                    }
                    if (h == 1)
                        h = 0;
                    else
                        h *= 6;
                    int region = (int)h;
                    double remainder = h - region;
                    double p = v * (1 - s);
                    double q = v * (1 - (s * remainder));
                    double t = v * (1 - (s * (1 - remainder)));
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

        public override Image<byte> ToUInt8()
        {
            ImageUInt8HSV uint8Image = new ImageUInt8HSV(Width, Height);
            double multiplier = (double)byte.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint8Image.H[x, y] = (byte)(H[x, y] * multiplier);
                    uint8Image.S[x, y] = (byte)(S[x, y] * multiplier);
                    uint8Image.V[x, y] = (byte)(V[x, y] * multiplier);
                }
            }
            return uint8Image;
        }

        public override Image<ushort> ToUInt16()
        {
            ImageUInt16HSV uint16Image = new ImageUInt16HSV(Width, Height);
            double multiplier = (double)ushort.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint16Image.H[x, y] = (ushort)(H[x, y] * multiplier);
                    uint16Image.S[x, y] = (ushort)(S[x, y] * multiplier);
                    uint16Image.V[x, y] = (ushort)(V[x, y] * multiplier);
                }
            }
            return uint16Image;
        }

        public override Image<uint> ToUInt32()
        {
            ImageUInt32HSV uint32Image = new ImageUInt32HSV(Width, Height);
            double multiplier = (double)uint.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint32Image.H[x, y] = (uint)(H[x, y] * multiplier);
                    uint32Image.S[x, y] = (uint)(S[x, y] * multiplier);
                    uint32Image.V[x, y] = (uint)(V[x, y] * multiplier);
                }
            }
            return uint32Image;
        }

        public override Image<double> ToDouble()
        {
            return (ImageDoubleHSV)Clone();
        }

        public override DataType GetDataType()
        {
            return DataType.Double;
        }
    }
}
