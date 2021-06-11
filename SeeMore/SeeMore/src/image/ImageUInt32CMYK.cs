namespace SeeMore
{
    internal class ImageUInt32CMYK : ImageCMYK<uint>
    {
        public ImageUInt32CMYK(uint width, uint height) : base(width, height)
        {
            C = new ChannelUInt32(width, height);
            M = new ChannelUInt32(width, height);
            Y = new ChannelUInt32(width, height);
            K = new ChannelUInt32(width, height);
        }

        public override Image ToRGB()
        {
            return ToDouble().ToRGB().ToUInt32();
        }

        public override Image ToUInt8()
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

        public override Image ToUInt16()
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

        public override Image ToUInt32()
        {
            return Clone();
        }

        public override Image ToDouble()
        {
            ImageDoubleCMYK doubleImage = new ImageDoubleCMYK(Width, Height);
            double divider = (double)uint.MaxValue + 1;
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
            return DataType.UInt32;
        }
    }
}
