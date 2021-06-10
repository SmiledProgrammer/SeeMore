namespace SeeMore
{
    public class ImageUInt32RGB : ImageRGB<uint>
    {
        public ImageUInt32RGB(uint width, uint height) : base(width, height)
        {
            R = new ChannelUInt32(width, height);
            G = new ChannelUInt32(width, height);
            B = new ChannelUInt32(width, height);
        }

        public override ImageHSV<uint> ToHSV()
        {
            return (ImageUInt32HSV)ToDouble().ToHSV().ToUInt32();
        }

        public override ImageCMYK<uint> ToCMYK()
        {
            return (ImageUInt32CMYK)ToDouble().ToCMYK().ToUInt32();
        }

        public override Image<byte> ToUInt8()
        {
            ImageUInt8RGB uint8Image = new ImageUInt8RGB(Width, Height);
            long divider = ((long)uint.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint8Image.R[x, y] = (byte)(R[x, y] / divider);
                    uint8Image.G[x, y] = (byte)(G[x, y] / divider);
                    uint8Image.B[x, y] = (byte)(B[x, y] / divider);
                }
            }
            return uint8Image;
        }

        public override Image<ushort> ToUInt16()
        {
            ImageUInt16RGB uint16Image = new ImageUInt16RGB(Width, Height);
            long divider = ((long)uint.MaxValue + 1) / (ushort.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint16Image.R[x, y] = (ushort)(R[x, y] / divider);
                    uint16Image.G[x, y] = (ushort)(G[x, y] / divider);
                    uint16Image.B[x, y] = (ushort)(B[x, y] / divider);
                }
            }
            return uint16Image;
        }

        public override Image<uint> ToUInt32()
        {
            return (ImageUInt32RGB)Clone();
        }

        public override Image<double> ToDouble()
        {
            ImageDoubleRGB doubleImage = (ImageDoubleRGB)ImageFactory.Create<double>(Width, Height, GetColorModel());
            double divider = (double)uint.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    doubleImage.R[x, y] = R[x, y] / divider;
                    doubleImage.G[x, y] = G[x, y] / divider;
                    doubleImage.B[x, y] = B[x, y] / divider;
                }
            }
            return doubleImage;
        }

        public override DataType GetDataType()
        {
            return DataType.UInt32;
        }
    }
}
