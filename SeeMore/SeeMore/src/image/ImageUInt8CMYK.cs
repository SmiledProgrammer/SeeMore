namespace SeeMore
{
    internal class ImageUInt8CMYK : ImageCMYK<byte>
    {
        public ImageUInt8CMYK(uint width, uint height) : base(width, height)
        {
            C = new ChannelUInt8(width, height);
            M = new ChannelUInt8(width, height);
            Y = new ChannelUInt8(width, height);
            K = new ChannelUInt8(width, height);
        }

        public override Image ToRGB()
        {
            ImageRGB<byte> rgbImage = new ImageUInt8RGB(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    byte c = C[x, y];
                    byte m = M[x, y];
                    byte ye = Y[x, y];
                    byte k = K[x, y];
                    rgbImage.R[x, y] = (byte)(byte.MaxValue * (1 - ((double)c / byte.MaxValue)) * (1 - (double)k / byte.MaxValue));
                    rgbImage.G[x, y] = (byte)(byte.MaxValue * (1 - ((double)m / byte.MaxValue)) * (1 - (double)k / byte.MaxValue));
                    rgbImage.B[x, y] = (byte)(byte.MaxValue * (1 - ((double)ye / byte.MaxValue)) * (1 - (double)k / byte.MaxValue));
                }
            }
            return rgbImage;
        }

        public override Image ToUInt8()
        {
            return Clone();
        }

        public override Image ToUInt16()
        {
            ImageUInt16CMYK uint16Image = new ImageUInt16CMYK(Width, Height);
            int multiplier = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint16Image.C[x, y] = (ushort)(C[x, y] * multiplier);
                    uint16Image.M[x, y] = (ushort)(M[x, y] * multiplier);
                    uint16Image.Y[x, y] = (ushort)(Y[x, y] * multiplier);
                    uint16Image.K[x, y] = (ushort)(K[x, y] * multiplier);
                }
            }
            return uint16Image;
        }

        public override Image ToUInt32()
        {
            ImageUInt32CMYK uint32Image = new ImageUInt32CMYK(Width, Height);
            long multiplier = ((long)uint.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint32Image.C[x, y] = (uint)(C[x, y] * multiplier);
                    uint32Image.M[x, y] = (uint)(M[x, y] * multiplier);
                    uint32Image.Y[x, y] = (uint)(Y[x, y] * multiplier);
                    uint32Image.K[x, y] = (uint)(K[x, y] * multiplier);
                }
            }
            return uint32Image;
        }

        public override Image ToDouble()
        {
            ImageDoubleCMYK doubleImage = new ImageDoubleCMYK(Width, Height);
            double divider = byte.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    doubleImage.C[x, y] = C[x, y] / divider;
                    doubleImage.M[x, y] = M[x, y] / divider;
                    doubleImage.Y[x, y] = Y[x, y] / divider;
                    doubleImage.K[x, y] = K[x, y] / divider;
                }
            }
            return doubleImage;
        }

        public override DataType GetDataType()
        {
            return DataType.UInt8;
        }
    }
}
