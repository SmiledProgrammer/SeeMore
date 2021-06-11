namespace SeeMore
{
    internal class ImageUInt8Gray : ImageGray<byte>
    {
        public ImageUInt8Gray(uint width, uint height) : base(width, height)
        {
            Gray = new ChannelUInt8(width, height);
        }

        public override void SetPixelValueFromDouble(int x, int y, double value)
        {
            Gray[x, y] = (byte)value;
        }

        public override Image ToUInt8()
        {
            return Clone();
        }

        public override Image ToUInt16()
        {
            ImageUInt16Gray uint16Image = new ImageUInt16Gray(Width, Height);
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

        public override Image ToUInt32()
        {
            ImageUInt32Gray uint32Image = new ImageUInt32Gray(Width, Height);
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

        public override Image ToDouble()
        {
            ImageDoubleGray doubleImage = new ImageDoubleGray(Width, Height);
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
