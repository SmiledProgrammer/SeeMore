using System;

namespace SeeMore
{
    public class ImageUInt32RGB : ImageRGB<uint>
    {
        public ImageUInt32RGB(uint width, uint height) : base(width, height)
        {
            R = new ChannelUInt32(width, height);
            G = new ChannelUInt32(width, height);
            B = new ChannelUInt32(width, height);
        }

        public override ImageHSV<uint> ToHSV()
        {
            ImageHSV<uint> hsvImage = new ImageUInt32HSV(Width, Height);
            long multiplier = ((long)uint.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint r = R[x, y];
                    uint g = G[x, y];
                    uint b = B[x, y];
                    uint min = Math.Min(r, Math.Min(g, b));
                    uint max = Math.Max(r, Math.Max(g, b));
                    hsvImage.V[x, y] = max;
                    if (max == 0)
                    {
                        hsvImage.S[x, y] = 0;
                        hsvImage.H[x, y] = 0;
                        continue;
                    }
                    uint diff = max - min;
                    uint saturation = (uint)(uint.MaxValue * (long)diff / max);
                    hsvImage.S[x, y] = saturation;
                    if (saturation == 0)
                    {
                        hsvImage.H[x, y] = 0;
                        continue;
                    }
                    if (max == r)
                        hsvImage.H[x, y] = (uint)(43 * (g - b) / diff * multiplier);
                    else if (max == g)
                        hsvImage.H[x, y] = (uint)((85 + 43 * (b - r) / diff) * multiplier);
                    else
                        hsvImage.H[x, y] = (uint)((171 + 43 * (r - g) / diff) * multiplier);
                }
            }
            return hsvImage;
        }

        public override ImageCMYK<uint> ToCMYK()
        {
            ImageCMYK<uint> cmykImage = new ImageUInt32CMYK(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint r = R[x, y];
                    uint g = G[x, y];
                    uint b = B[x, y];
                    uint max = Math.Max(r, Math.Max(g, b));
                    if (max == 0)
                    {
                        cmykImage.C[x, y] = 0;
                        cmykImage.M[x, y] = 0;
                        cmykImage.Y[x, y] = 0;
                        cmykImage.K[x, y] = ushort.MaxValue;
                        continue;
                    }
                    double k = 1 - ((double)max / uint.MaxValue);
                    cmykImage.K[x, y] = (uint)(uint.MaxValue * k);
                    cmykImage.C[x, y] = (uint)(uint.MaxValue * ((1 - ((double)r / uint.MaxValue) - k) / (1 - k)));
                    cmykImage.M[x, y] = (uint)(uint.MaxValue * ((1 - ((double)g / uint.MaxValue) - k) / (1 - k)));
                    cmykImage.Y[x, y] = (uint)(uint.MaxValue * ((1 - ((double)b / uint.MaxValue) - k) / (1 - k)));
                }
            }
            return cmykImage;
        }

        public override Image<byte> ToUInt8()
        {
            ImageUInt8RGB uint8Image = new ImageUInt8RGB(Width, Height);
            long divider = ((long)uint.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint8Image.R[x, y] = (byte)(R[x, y] / divider);
                    uint8Image.G[x, y] = (byte)(G[x, y] / divider);
                    uint8Image.B[x, y] = (byte)(B[x, y] / divider);
                }
            }
            return uint8Image;
        }

        public override Image<ushort> ToUInt16()
        {
            ImageUInt16RGB uint16Image = new ImageUInt16RGB(Width, Height);
            long divider = ((long)uint.MaxValue + 1) / (ushort.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint16Image.R[x, y] = (ushort)(R[x, y] / divider);
                    uint16Image.G[x, y] = (ushort)(G[x, y] / divider);
                    uint16Image.B[x, y] = (ushort)(B[x, y] / divider);
                }
            }
            return uint16Image;
        }

        public override Image<uint> ToUInt32()
        {
            return (ImageUInt32RGB)Clone();
        }

        public override Image<double> ToDouble()
        {
            throw new NotImplementedException();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt32;
        }
    }
}
