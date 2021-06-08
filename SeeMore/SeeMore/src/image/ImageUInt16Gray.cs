namespace SeeMore
{
    public class ImageUInt16Gray : ImageGray<ushort>
    {
        public ImageUInt16Gray(uint width, uint height) : base(width, height)
        {
            Gray = new ChannelUInt16(width, height);
        }

        public override void SetPixelValueFromDouble(int x, int y, double value)
        {
            Gray[x, y] = (ushort)value;
        }

        public override Image<byte> ToUInt8()
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

        public override Image<ushort> ToUInt16()
        {
            return (ImageUInt16Gray)Clone();
        }

        public override Image<uint> ToUInt32()
        {
            throw new System.NotImplementedException();
        }

        public override Image<double> ToDouble()
        {
            throw new System.NotImplementedException();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt16;
        }
    }
}
