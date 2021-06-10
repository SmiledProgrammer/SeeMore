﻿namespace SeeMore
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
            return (ImageUInt16RGB)ToDouble().ToRGB().ToUInt16();
        }

        public override Image<byte> ToUInt8()
        {
            ImageUInt8CMYK uint8Image = new ImageUInt8CMYK(Width, Height);
            int divider = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint8Image.C[x, y] = (byte)(C[x, y] / divider);
                    uint8Image.M[x, y] = (byte)(M[x, y] / divider);
                    uint8Image.Y[x, y] = (byte)(Y[x, y] / divider);
                    uint8Image.K[x, y] = (byte)(K[x, y] / divider);
                }
            }
            return uint8Image;
        }

        public override Image<ushort> ToUInt16()
        {
            return (ImageUInt16CMYK)Clone();
        }

        public override Image<uint> ToUInt32()
        {
            ImageUInt32CMYK uint32Image = (ImageUInt32CMYK)ImageFactory.Create<uint>(Width, Height, GetColorModel());
            long multiplier = ((long)uint.MaxValue + 1) / (ushort.MaxValue + 1);
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
            ImageDoubleCMYK doubleImage = (ImageDoubleCMYK)ImageFactory.Create<double>(Width, Height, GetColorModel());
            double divider = ushort.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    doubleImage.C[x, y] = C[x, y] / divider;
                    doubleImage.M[x, y] = M[x, y] / divider;
                    doubleImage.Y[x, y] = Y[x, y] / divider;
                    doubleImage.K[x, y] = K[x, y] / divider;
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
