namespace SeeMore
{
    public class ImageUInt32CMYK : ImageCMYK<uint>
    {
        public ImageUInt32CMYK(uint width, uint height) : base(width, height)
        {
            C = new ChannelUInt32(width, height);
            M = new ChannelUInt32(width, height);
            Y = new ChannelUInt32(width, height);
            K = new ChannelUInt32(width, height);
        }

        public override ImageRGB<uint> ToRGB()
        {
            ImageRGB<uint> rgbImage = new ImageUInt32RGB(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint c = C[x, y];
                    uint m = M[x, y];
                    uint ye = Y[x, y];
                    uint k = K[x, y];
                    rgbImage.R[x, y] = (uint)(uint.MaxValue * (1 - ((double)c / uint.MaxValue)) * (1 - (double)k / uint.MaxValue));
                    rgbImage.G[x, y] = (uint)(uint.MaxValue * (1 - ((double)m / uint.MaxValue)) * (1 - (double)k / uint.MaxValue));
                    rgbImage.B[x, y] = (uint)(uint.MaxValue * (1 - ((double)ye / uint.MaxValue)) * (1 - (double)k / uint.MaxValue));
                }
            }
            return rgbImage;
        }

        public override Image<byte> ToUInt8()
        {
            ImageUInt8CMYK uint8Image = new ImageUInt8CMYK(Width, Height);
            long divider = ((long)uint.MaxValue + 1) / (byte.MaxValue + 1);
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
            ImageUInt16CMYK uint16Image = new ImageUInt16CMYK(Width, Height);
            long divider = ((long)uint.MaxValue + 1) / (ushort.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint16Image.C[x, y] = (ushort)(C[x, y] / divider);
                    uint16Image.M[x, y] = (ushort)(M[x, y] / divider);
                    uint16Image.Y[x, y] = (ushort)(Y[x, y] / divider);
                    uint16Image.K[x, y] = (ushort)(K[x, y] / divider);
                }
            }
            return uint16Image;
        }

        public override Image<uint> ToUInt32()
        {
            return (ImageUInt32CMYK)Clone();
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
