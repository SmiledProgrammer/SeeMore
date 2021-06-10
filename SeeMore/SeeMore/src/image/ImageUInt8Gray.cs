namespace SeeMore
{
    public class ImageUInt8Gray : ImageGray<byte>
    {
        public ImageUInt8Gray(uint width, uint height) : base(width, height)
        {
            Gray = new ChannelUInt8(width, height);
        }

        public override void SetPixelValueFromDouble(int x, int y, double value)
        {
            Gray[x, y] = (byte)value;
        }

        public override Image<byte> ToUInt8()
        {
            return (ImageUInt8Gray)Clone();
        }

        public override Image<ushort> ToUInt16()
        {
            ImageUInt16Gray uint16Image = (ImageUInt16Gray)ImageFactory.Create<ushort>(Width, Height, GetColorModel());
            int multiplier = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint16Image.Gray[x, y] = (ushort)(Gray[x, y] * multiplier);
                }
            }
            return uint16Image;
        }

        public override Image<uint> ToUInt32()
        {
            ImageUInt32Gray uint32Image = (ImageUInt32Gray)ImageFactory.Create<uint>(Width, Height, GetColorModel());
            long multiplier = ((long)uint.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint32Image.Gray[x, y] = (uint)(Gray[x, y] * multiplier);
                }
            }
            return uint32Image;
        }

        public override Image<double> ToDouble()
        {
            ImageDoubleGray doubleImage = (ImageDoubleGray)ImageFactory.Create<double>(Width, Height, GetColorModel());
            double divider = byte.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    doubleImage.Gray[x, y] = Gray[x, y] / divider;
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
