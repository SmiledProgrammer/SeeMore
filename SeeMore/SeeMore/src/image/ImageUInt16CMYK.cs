namespace SeeMore
{
    public class ImageUInt16CMYK : ImageCMYK<ushort>
    {
        public ImageUInt16CMYK(uint width, uint height) : base(width, height)
        {
            C = new ChannelUInt16(width, height);
            M = new ChannelUInt16(width, height);
            Y = new ChannelUInt16(width, height);
            K = new ChannelUInt16(width, height);
        }

        public override ImageRGB<ushort> ToRGB()
        {
            ImageRGB<ushort> rgbImage = new ImageUInt16RGB(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ushort c = C[x, y];
                    ushort m = M[x, y];
                    ushort ye = Y[x, y];
                    ushort k = K[x, y];
                    rgbImage.R[x, y] = (ushort)(ushort.MaxValue * (1 - ((double)c / ushort.MaxValue)) * (1 - (double)k / ushort.MaxValue));
                    rgbImage.G[x, y] = (ushort)(ushort.MaxValue * (1 - ((double)m / ushort.MaxValue)) * (1 - (double)k / ushort.MaxValue));
                    rgbImage.B[x, y] = (ushort)(ushort.MaxValue * (1 - ((double)ye / ushort.MaxValue)) * (1 - (double)k / ushort.MaxValue));
                }
            }
            return rgbImage;
        }

        public override Image<byte> ToUInt8()
        {
            ImageUInt8CMYK uint8Image = new ImageUInt8CMYK(Width, Height);
            int divider = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint8Image.C[x, y] = (byte)(C[x, y] / divider);
                    uint8Image.M[x, y] = (byte)(M[x, y] / divider);
                    uint8Image.Y[x, y] = (byte)(Y[x, y] / divider);
                    uint8Image.K[x, y] = (byte)(K[x, y] / divider);
                }
            }
            return uint8Image;
        }

        public override Image<ushort> ToUInt16()
        {
            return (ImageUInt16CMYK)Clone();
        }

        public override Image<uint> ToUInt32()
        {
            ImageUInt32CMYK uint32Image = (ImageUInt32CMYK)ImageFactory.Create<uint>(Width, Height, GetColorModel());
            long multiplier = ((long)uint.MaxValue + 1) / (ushort.MaxValue + 1);
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

        public override Image<double> ToDouble()
        {
            throw new System.NotImplementedException();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt16;
        }
    }
}
