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
            return (ImageUInt32RGB)ToDouble().ToRGB().ToUInt32();
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
            ImageDoubleHSV doubleImage = (ImageDoubleHSV)ImageFactory.Create<double>(Width, Height, GetColorModel());
            double divider = (double)uint.MaxValue + 1;
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
            return DataType.UInt32;
        }
    }
}
