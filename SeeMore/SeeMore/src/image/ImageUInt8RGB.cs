using System;

namespace SeeMore
{
    public class ImageUInt8RGB : ImageRGB<byte>
    {
        public ImageUInt8RGB(uint width, uint height) : base(width, height)
        {
            R = new ChannelUInt8(width, height);
            G = new ChannelUInt8(width, height);
            B = new ChannelUInt8(width, height);
        }

        public override ImageHSV<byte> ToHSV()
        {
            ImageHSV<byte> hsvImage = new ImageUInt8HSV(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    byte r = R[x, y];
                    byte g = G[x, y];
                    byte b = B[x, y];
                    byte min = Math.Min(r, Math.Min(g, b));
                    byte max = Math.Max(r, Math.Max(g, b));
                    hsvImage.V[x, y] = max;
                    if (max == 0)
                    {
                        hsvImage.S[x, y] = 0;
                        hsvImage.H[x, y] = 0;
                        continue;
                    }
                    byte diff = (byte)(max - min);
                    byte saturation = (byte)(byte.MaxValue * (long)diff / max);
                    hsvImage.S[x, y] = saturation;
                    if (saturation == 0)
                    {
                        hsvImage.H[x, y] = 0;
                        continue;
                    }
                    if (max == r)
                        hsvImage.H[x, y] = (byte)(43 * (g - b) / diff);
                    else if (max == g)
                        hsvImage.H[x, y] = (byte)(85 + 43 * (b - r) / diff);
                    else
                        hsvImage.H[x, y] = (byte)(171 + 43 * (r - g) / diff);
                }
            }
            return hsvImage;
        }

        public override ImageCMYK<byte> ToCMYK()
        {
            ImageCMYK<byte> cmykImage = new ImageUInt8CMYK(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    byte r = R[x, y];
                    byte g = G[x, y];
                    byte b = B[x, y];
                    byte max = Math.Max(r, Math.Max(g, b));
                    if (max == 0)
                    {
                        cmykImage.C[x, y] = 0;
                        cmykImage.M[x, y] = 0;
                        cmykImage.Y[x, y] = 0;
                        cmykImage.K[x, y] = byte.MaxValue;
                        continue;
                    }
                    double k = 1.0 - ((double)max / byte.MaxValue);
                    cmykImage.K[x, y] = (byte)(byte.MaxValue * k);
                    cmykImage.C[x, y] = (byte)(byte.MaxValue * ((1 - ((double)r / byte.MaxValue) - k) / (1 - k)));
                    cmykImage.M[x, y] = (byte)(byte.MaxValue * ((1 - ((double)g / byte.MaxValue) - k) / (1 - k)));
                    cmykImage.Y[x, y] = (byte)(byte.MaxValue * ((1 - ((double)b / byte.MaxValue) - k) / (1 - k)));
                }
            }
            return cmykImage;
        }

        public override Image<byte> ToUInt8()
        {
            return (ImageUInt8RGB)Clone();
        }

        public override Image<ushort> ToUInt16()
        {
            ImageUInt16RGB uint16Image = (ImageUInt16RGB)ImageFactory.Create<ushort>(Width, Height, GetColorModel());
            int multiplier = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint16Image.R[x, y] = (ushort)(R[x, y] * multiplier);
                    uint16Image.G[x, y] = (ushort)(G[x, y] * multiplier);
                    uint16Image.B[x, y] = (ushort)(B[x, y] * multiplier);
                }
            }
            return uint16Image;
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
            return DataType.UInt8;
        }
    }
}
