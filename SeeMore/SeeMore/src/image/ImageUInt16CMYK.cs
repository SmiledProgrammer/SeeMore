namespace SeeMore
{
    public class ImageUInt16CMYK : ImageCMYK<ushort>
    {
        public ImageUInt16CMYK(uint width, uint height) : base(width, height)
        {
            C = new ChannelUInt16(width, height);
            M = new ChannelUInt16(width, height);
            Y = new ChannelUInt16(width, height);
            K = new ChannelUInt16(width, height);
        }

        public override ImageRGB<ushort> ToRGB()
        {
            ImageRGB<ushort> rgbImage = new ImageUInt16RGB(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ushort c = C[x, y];
                    ushort m = M[x, y];
                    ushort ye = Y[x, y];
                    ushort k = K[x, y];
                    rgbImage.R[x, y] = (ushort)(ushort.MaxValue * (1 - ((double)c / ushort.MaxValue)) * (1 - (double)k / ushort.MaxValue));
                    rgbImage.G[x, y] = (ushort)(ushort.MaxValue * (1 - ((double)m / ushort.MaxValue)) * (1 - (double)k / ushort.MaxValue));
                    rgbImage.B[x, y] = (ushort)(ushort.MaxValue * (1 - ((double)ye / ushort.MaxValue)) * (1 - (double)k / ushort.MaxValue));
                }
            }
            return rgbImage;
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
