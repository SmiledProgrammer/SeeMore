namespace SeeMore
{
    public class ImageDoubleGray : ImageGray<double>
    {
        public ImageDoubleGray(uint width, uint height) : base(width, height)
        {
            Gray = new ChannelDouble(width, height);
        }

        public override void SetPixelValueFromDouble(int x, int y, double value)
        {
            Gray[x, y] = value;
        }

        public override Image<byte> ToUInt8()
        {
            ImageUInt8Gray uint8Image = new ImageUInt8Gray(Width, Height);
            double multiplier = (double)byte.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint8Image.Gray[x, y] = (byte)(Gray[x, y] * multiplier);
                }
            }
            return uint8Image;
        }

        public override Image<ushort> ToUInt16()
        {
            ImageUInt16Gray uint16Image = new ImageUInt16Gray(Width, Height);
            double multiplier = (double)ushort.MaxValue + 1;
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
            ImageUInt32Gray uint32Image = new ImageUInt32Gray(Width, Height);
            double multiplier = (double)uint.MaxValue + 1;
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
            return (ImageDoubleGray)Clone();
        }

        public override DataType GetDataType()
        {
            return DataType.Double;
        }
    }
}
