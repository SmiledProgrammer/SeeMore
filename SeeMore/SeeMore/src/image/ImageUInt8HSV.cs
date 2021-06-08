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
                        continue;
                    }
                    byte region = (byte)(h / 43);
                    byte remainder = (byte)((h - (region * 43)) * 6);
                    byte p = (byte)((v * (byte.MaxValue - s)) >> 8);
                    byte q = (byte)((v * (byte.MaxValue - ((s * remainder) >> 8))) >> 8);
                    byte t = (byte)((v * (byte.MaxValue - ((s * (byte.MaxValue - remainder)) >> 8))) >> 8);
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
            return (ImageUInt8HSV)Clone();
        }

        public override Image<ushort> ToUInt16()
        {
            ImageUInt16HSV uint16Image = (ImageUInt16HSV)ImageFactory.Create<ushort>(Width, Height, GetColorModel());
            int multiplier = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
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
            throw new System.NotImplementedException();
        }

        public override Image<double> ToDouble()
        {
            throw new System.NotImplementedException();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt8;
        }
    }
}
