namespace SeeMore
{
    public class ImageUInt8CMYK : ImageCMYK<byte>
    {
        public ImageUInt8CMYK(uint width, uint height) : base(width, height)
        {
            C = new ChannelUInt8(width, height);
            M = new ChannelUInt8(width, height);
            Y = new ChannelUInt8(width, height);
            K = new ChannelUInt8(width, height);
        }

        public override ImageRGB<byte> ToRGB()
        {
            ImageRGB<byte> rgbImage = new ImageUInt8RGB(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    byte c = C[x, y];
                    byte m = M[x, y];
                    byte ye = Y[x, y];
                    byte k = K[x, y];
                    rgbImage.R[x, y] = (byte)(byte.MaxValue * (1 - ((double)c / byte.MaxValue)) * (1 - (double)k / byte.MaxValue));
                    rgbImage.G[x, y] = (byte)(byte.MaxValue * (1 - ((double)m / byte.MaxValue)) * (1 - (double)k / byte.MaxValue));
                    rgbImage.B[x, y] = (byte)(byte.MaxValue * (1 - ((double)ye / byte.MaxValue)) * (1 - (double)k / byte.MaxValue));
                }
            }
            return rgbImage;
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
