namespace SeeMore
{
    internal class ImageUInt16RGB : ImageRGB<ushort>
    {
        public ImageUInt16RGB(uint width, uint height) : base(width, height)
        {
            R = new ChannelUInt16(width, height);
            G = new ChannelUInt16(width, height);
            B = new ChannelUInt16(width, height);
        }

        public override Image ToHSV()
        {
            return ToDouble().ToHSV().ToUInt16();
        }

        public override Image ToCMYK()
        {
            return ToDouble().ToCMYK().ToUInt16();
        }

        public override Image ToUInt8()
        {
            ImageUInt8RGB uint8Image = new ImageUInt8RGB(Width, Height);
            int divider = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
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

        public override Image ToUInt16()
        {
            return Clone();
        }

        public override Image ToUInt32()
        {
            ImageUInt32RGB uint32Image = new ImageUInt32RGB(Width, Height);
            long multiplier = ((long)uint.MaxValue + 1) / (ushort.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint32Image.R[x, y] = (uint)(R[x, y] * multiplier);
                    uint32Image.G[x, y] = (uint)(G[x, y] * multiplier);
                    uint32Image.B[x, y] = (uint)(B[x, y] * multiplier);
                }
            }
            return uint32Image;
        }

        public override Image ToDouble()
        {
            ImageDoubleRGB doubleImage = new ImageDoubleRGB(Width, Height);
            double divider = ushort.MaxValue + 1;
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
            return DataType.UInt16;
        }
    }
}
