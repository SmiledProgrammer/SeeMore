namespace SeeMore
{
    internal class ImageUInt32Gray : ImageGray<uint>
    {
        public ImageUInt32Gray(uint width, uint height) : base(width, height)
        {
            Gray = new ChannelUInt32(width, height);
        }

        public override void SetPixelValueFromDouble(int x, int y, double value)
        {
            Gray[x, y] = (uint)value;
        }

        public override Image ToUInt8()
        {
            ImageUInt8Gray uint8Image = new ImageUInt8Gray(Width, Height);
            long divider = ((long)uint.MaxValue + 1) / (byte.MaxValue + 1);
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
            ImageUInt16Gray uint16Image = new ImageUInt16Gray(Width, Height);
            long divider = ((long)uint.MaxValue + 1) / (ushort.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint16Image.Gray[x, y] = (ushort)(Gray[x, y] / divider);
                }
            }
            return uint16Image;
        }

        public override Image ToUInt32()
        {
            return (ImageUInt32Gray)Clone();
        }

        public override Image ToDouble()
        {
            ImageDoubleGray doubleImage = new ImageDoubleGray(Width, Height);
            double divider = (double)uint.MaxValue + 1;
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
            return DataType.UInt32;
        }
    }
}
