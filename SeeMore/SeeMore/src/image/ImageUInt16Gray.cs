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

        public override ImageUInt8RGB ToByteRGBImage()
        {
            return ToRGB().ToByteRGBImage();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt16;
        }
    }
}
