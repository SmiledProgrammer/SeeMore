namespace SeeMore
{
    internal class ImageUInt16HSV : ImageHSV<ushort>
    {
        public ImageUInt16HSV(uint width, uint height) : base(width, height)
        {
            H = new ChannelUInt16(width, height);
            S = new ChannelUInt16(width, height);
            V = new ChannelUInt16(width, height);
        }

        public override Image ToRGB()
        {
            return ToDouble().ToRGB().ToUInt16();
        }

        public override Image ToUInt8()
        {
            ImageUInt8HSV uint8Image = new ImageUInt8HSV(Width, Height);
            int divider = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
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

        public override Image ToUInt16()
        {
            return Clone();
        }

        public override Image ToUInt32()
        {
            ImageUInt32HSV uint32Image = new ImageUInt32HSV(Width, Height);
            long multiplier = ((long)uint.MaxValue + 1) / (ushort.MaxValue + 1);
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

        public override Image ToDouble()
        {
            ImageDoubleHSV doubleImage = new ImageDoubleHSV(Width, Height);
            double divider = ushort.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    doubleImage.H[x, y] = H[x, y] / divider;
                    doubleImage.S[x, y] = S[x, y] / divider;
                    doubleImage.V[x, y] = V[x, y] / divider;
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
