using System;

namespace SeeMore
{
    public class ImageUInt16RGB : ImageRGB<ushort>
    {
        public ImageUInt16RGB(uint width, uint height) : base(width, height)
        {
            R = new ChannelUInt16(width, height);
            G = new ChannelUInt16(width, height);
            B = new ChannelUInt16(width, height);
        }

        public override ImageHSV<ushort> ToHSV()
        {
            ImageHSV<ushort> hsvImage = new ImageUInt16HSV(Width, Height);
            int multiplier = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ushort r = R[x, y];
                    ushort g = G[x, y];
                    ushort b = B[x, y];
                    ushort min = Math.Min(r, Math.Min(g, b));
                    ushort max = Math.Max(r, Math.Max(g, b));
                    hsvImage.V[x, y] = max;
                    if (max == 0)
                    {
                        hsvImage.S[x, y] = 0;
                        hsvImage.H[x, y] = 0;
                        continue;
                    }
                    ushort diff = (ushort)(max - min);
                    ushort saturation = (ushort)(ushort.MaxValue * (long)diff / max);
                    hsvImage.S[x, y] = saturation;
                    if (saturation == 0)
                    {
                        hsvImage.H[x, y] = 0;
                        continue;
                    }
                    if (max == r)
                        hsvImage.H[x, y] = (ushort)(43 * (g - b) / diff * multiplier);
                    else if (max == g)
                        hsvImage.H[x, y] = (ushort)((85 + 43 * (b - r) / diff) * multiplier);
                    else
                        hsvImage.H[x, y] = (ushort)((171 + 43 * (r - g) / diff) * multiplier);
                }
            }
            return hsvImage;
        }

        public override ImageCMYK<ushort> ToCMYK()
        {
            ImageCMYK<ushort> cmykImage = new ImageUInt16CMYK(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ushort r = R[x, y];
                    ushort g = G[x, y];
                    ushort b = B[x, y];
                    ushort max = Math.Max(r, Math.Max(g, b));
                    if (max == 0)
                    {
                        cmykImage.C[x, y] = 0;
                        cmykImage.M[x, y] = 0;
                        cmykImage.Y[x, y] = 0;
                        cmykImage.K[x, y] = ushort.MaxValue;
                        continue;
                    }
                    double k = 1 - ((double)max / ushort.MaxValue);
                    cmykImage.K[x, y] = (ushort)(ushort.MaxValue * k);
                    cmykImage.C[x, y] = (ushort)(ushort.MaxValue * ((1 - ((double)r / ushort.MaxValue) - k) / (1 - k)));
                    cmykImage.M[x, y] = (ushort)(ushort.MaxValue * ((1 - ((double)g / ushort.MaxValue) - k) / (1 - k)));
                    cmykImage.Y[x, y] = (ushort)(ushort.MaxValue * ((1 - ((double)b / ushort.MaxValue) - k) / (1 - k)));
                }
            }
            return cmykImage;
        }

        public override Image<byte> ToUInt8()
        {
            ImageUInt8RGB uint8Image = new ImageUInt8RGB(Width, Height);
            int divider = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
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
            return (ImageUInt16RGB)Clone();
        }

        public override Image<uint> ToUInt32()
        {
            throw new NotImplementedException();
        }

        public override Image<double> ToDouble()
        {
            throw new NotImplementedException();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt16;
        }
    }
}
