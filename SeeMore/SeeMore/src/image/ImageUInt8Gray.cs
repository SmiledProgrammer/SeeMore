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

        public override ImageUInt8RGB ToByteRGBImage()
        {
            return (ImageUInt8RGB)ToRGB();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt8;
        }
    }
}
