namespace SeeMore
{
    internal class ImageUInt16Gray : ImageGray<ushort>
    {
        public ImageUInt16Gray(uint width, uint height) : base(width, height)
        {
            Gray = new ChannelUInt16(width, height);
        }

        public override void SetPixelValueFromDouble(int x, int y, double value)
        {
            Gray[x, y] = (ushort)value;
        }

        public override Image ToUInt8()
        {
            ImageUInt8Gray uint8Image = new ImageUInt8Gray(Width, Height);
            int divider = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint8Image.Gray[x, y] = (byte)(Gray[x, y] / divider);
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
            ImageUInt32Gray uint32Image = new ImageUInt32Gray(Width, Height);
            long multiplier = ((long)uint.MaxValue + 1) / (ushort.MaxValue + 1);
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
            double divider = ushort.MaxValue + 1;
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
            return DataType.UInt16;
        }
    }
}
