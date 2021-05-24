namespace SeeMore
{
    public class ImageUInt8Gray : ImageGray<byte>
    {
        public ImageUInt8Gray(uint width, uint height) : base(width, height)
        {
            Gray = new ChannelUInt8(width, height);
        }

        public override ImageRGB<byte> ToRGB()
        {
            ImageUInt8RGB result = new ImageUInt8RGB(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    result.R[x, y] = Gray[x, y];
                    result.G[x, y] = Gray[x, y];
                    result.B[x, y] = Gray[x, y];
                }
            }
            return result;
        }

        public override ImageHSV<byte> ToHSV()
        {
            // TODO
            return null;
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
