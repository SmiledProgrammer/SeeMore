namespace SeeMore
{
    public class ImageDoubleCMYK : ImageCMYK<double>
    {
        public ImageDoubleCMYK(uint width, uint height) : base(width, height)
        {
            C = new ChannelDouble(width, height);
            M = new ChannelDouble(width, height);
            Y = new ChannelDouble(width, height);
            K = new ChannelDouble(width, height);
        }

        public override ImageRGB<double> ToRGB()
        {
            ImageRGB<double> rgbImage = new ImageDoubleRGB(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    double c = C[x, y];
                    double m = M[x, y];
                    double ye = Y[x, y];
                    double k = K[x, y];
                    rgbImage.R[x, y] = (1 - c) * (1 - k);
                    rgbImage.G[x, y] = (1 - m) * (1 - k);
                    rgbImage.B[x, y] = (1 - ye) * (1 - k);
                }
            }
            return rgbImage;
        }

        public override Image<byte> ToUInt8()
        {
            ImageUInt8CMYK uint8Image = new ImageUInt8CMYK(Width, Height);
            double multiplier = (double)byte.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint8Image.C[x, y] = (byte)(C[x, y] * multiplier);
                    uint8Image.M[x, y] = (byte)(M[x, y] * multiplier);
                    uint8Image.Y[x, y] = (byte)(Y[x, y] * multiplier);
                    uint8Image.K[x, y] = (byte)(K[x, y] * multiplier);
                }
            }
            return uint8Image;
        }

        public override Image<ushort> ToUInt16()
        {
            ImageUInt16CMYK uint16Image = new ImageUInt16CMYK(Width, Height);
            double multiplier = (double)ushort.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint16Image.C[x, y] = (ushort)(C[x, y] * multiplier);
                    uint16Image.M[x, y] = (ushort)(M[x, y] * multiplier);
                    uint16Image.Y[x, y] = (ushort)(Y[x, y] * multiplier);
                    uint16Image.K[x, y] = (ushort)(K[x, y] * multiplier);
                }
            }
            return uint16Image;
        }

        public override Image<uint> ToUInt32()
        {
            ImageUInt32CMYK uint32Image = new ImageUInt32CMYK(Width, Height);
            double multiplier = (double)uint.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint32Image.C[x, y] = (uint)(C[x, y] * multiplier);
                    uint32Image.M[x, y] = (uint)(M[x, y] * multiplier);
                    uint32Image.Y[x, y] = (uint)(Y[x, y] * multiplier);
                    uint32Image.K[x, y] = (uint)(K[x, y] * multiplier);
                }
            }
            return uint32Image;
        }

        public override Image<double> ToDouble()
        {
            return (ImageDoubleCMYK)Clone();
        }

        public override DataType GetDataType()
        {
            return DataType.Double;
        }
    }
}
