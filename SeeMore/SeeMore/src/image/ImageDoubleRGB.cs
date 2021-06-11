using System;

namespace SeeMore
{
    internal class ImageDoubleRGB : ImageRGB<double>
    {
        public ImageDoubleRGB(uint width, uint height) : base(width, height)
        {
            R = new ChannelDouble(width, height);
            G = new ChannelDouble(width, height);
            B = new ChannelDouble(width, height);
        }

        public override Image ToHSV()
        {
            ImageHSV<double> hsvImage = new ImageDoubleHSV(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    double r = R[x, y];
                    double g = G[x, y];
                    double b = B[x, y];
                    double min = Math.Min(r, Math.Min(g, b));
                    double max = Math.Max(r, Math.Max(g, b));
                    hsvImage.V[x, y] = max;
                    double diff = max - min;

                    if (diff < 0.00001)
                    {
                        hsvImage.S[x, y] = 0;
                        hsvImage.H[x, y] = 0;
                        continue;
                    }

                    if (max > 0)
                    {
                        hsvImage.S[x, y] = diff / max;
                    }
                    else
                    {
                        hsvImage.S[x, y] = 0;
                        hsvImage.H[x, y] = 0;
                        continue;
                    }

                    if (r >= max)
                        hsvImage.H[x, y] = (g - b) / diff / 6;
                    else if (g >= max)
                        hsvImage.H[x, y] = (2 + (b - r) / diff) / 6;
                    else
                        hsvImage.H[x, y] = (4 + (r - g) / diff) / 6;

                    if (hsvImage.H[x, y] < 0)
                    {
                        hsvImage.H[x, y] += 1;
                    }
                }
            }
            return hsvImage;
        }

        public override Image ToCMYK()
        {
            ImageCMYK<double> cmykImage = new ImageDoubleCMYK(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    double r = R[x, y];
                    double g = G[x, y];
                    double b = B[x, y];
                    double max = Math.Max(r, Math.Max(g, b));
                    double k = 1.0 - max;
                    cmykImage.K[x, y] = k;
                    cmykImage.C[x, y] = (1 - r - k) / (1 - k);
                    cmykImage.M[x, y] = (1 - g - k) / (1 - k);
                    cmykImage.Y[x, y] = (1 - b - k) / (1 - k);
                }
            }
            return cmykImage;
        }

        public override Image ToUInt8()
        {
            ImageUInt8RGB uint8Image = new ImageUInt8RGB(Width, Height);
            double multiplier = (double)byte.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint8Image.R[x, y] = (byte)(R[x, y] * multiplier);
                    uint8Image.G[x, y] = (byte)(G[x, y] * multiplier);
                    uint8Image.B[x, y] = (byte)(B[x, y] * multiplier);
                }
            }
            return uint8Image;
        }

        public override Image ToUInt16()
        {
            ImageUInt16RGB uint16Image = new ImageUInt16RGB(Width, Height);
            double multiplier = (double)ushort.MaxValue + 1;
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

        public override Image ToUInt32()
        {
            ImageUInt32RGB uint32Image = new ImageUInt32RGB(Width, Height);
            double multiplier = (double)uint.MaxValue + 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    uint32Image.R[x, y] = (uint)(R[x, y] * multiplier);
                    uint32Image.G[x, y] = (uint)(G[x, y] * multiplier);
                    uint32Image.B[x, y] = (uint)(B[x, y] * multiplier);
                }
            }
            return uint32Image;
        }

        public override Image ToDouble()
        {
            return (ImageDoubleRGB)Clone();
        }

        public override DataType GetDataType()
        {
            return DataType.Double;
        }
    }
}
