namespace SeeMore
{
    public class ImageUInt32HSV : ImageHSV<uint>
    {
        public ImageUInt32HSV(uint width, uint height) : base(width, height)
        {
            H = new ChannelUInt32(width, height);
            S = new ChannelUInt32(width, height);
            V = new ChannelUInt32(width, height);
        }

        public override ImageRGB<uint> ToRGB()
        {
            ImageRGB<uint> rgbImage = new ImageUInt32RGB(Width, Height);
            long multiplier = ((long)uint.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint h = H[x, y];
                    uint s = S[x, y];
                    uint v = V[x, y];
                    if (s == 0)
                    {
                        rgbImage.R[x, y] = v;
                        rgbImage.G[x, y] = v;
                        rgbImage.B[x, y] = v;
                        continue;
                    }
                    uint region = (uint)(h / (43 * multiplier));
                    uint remainder = (uint)((h - (region * 43* multiplier)) * 6);
                    uint p = ((v * (uint.MaxValue - s)) >> 16);
                    uint q = ((v * (uint.MaxValue - ((s * remainder) >> 16))) >> 16);
                    uint t = ((v * (uint.MaxValue - ((s * (uint.MaxValue - remainder)) >> 16))) >> 16);
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
            long divider = ((long)uint.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint8Image.H[x, y] = (byte)(H[x, y] / divider);
                    uint8Image.S[x, y] = (byte)(S[x, y] / divider);
                    uint8Image.V[x, y] = (byte)(V[x, y] / divider);
                }
            }
            return uint8Image;
        }

        public override Image<ushort> ToUInt16()
        {
            ImageUInt16HSV uint16Image = new ImageUInt16HSV(Width, Height);
            long divider = ((long)uint.MaxValue + 1) / (ushort.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint16Image.H[x, y] = (ushort)(H[x, y] / divider);
                    uint16Image.S[x, y] = (ushort)(S[x, y] / divider);
                    uint16Image.V[x, y] = (ushort)(V[x, y] / divider);
                }
            }
            return uint16Image;
        }

        public override Image<uint> ToUInt32()
        {
            return (ImageUInt32HSV)Clone();
        }

        public override Image<double> ToDouble()
        {
            throw new System.NotImplementedException();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt32;
        }
    }
}
