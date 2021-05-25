﻿using System;

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
            for (uint x = 0; x < Width; x++)
            {
                for (uint y = 0; y < Height; y++)
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
                    byte saturation = (byte)(255 * (long)diff / max);
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
            for (uint x = 0; x < Width; x++)
            {
                for (uint y = 0; y < Height; y++)
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
                        cmykImage.K[x, y] = 255;
                        continue;
                    }
                    double k = 1.0 - (max / 255.0);
                    cmykImage.K[x, y] = (byte)(255 * k);
                    cmykImage.C[x, y] = (byte)(255 * ((1 - (r / 255.0) - k) / (1 - k)));
                    cmykImage.M[x, y] = (byte)(255 * ((1 - (g / 255.0) - k) / (1 - k)));
                    cmykImage.Y[x, y] = (byte)(255 * ((1 - (b / 255.0) - k) / (1 - k)));
                }
            }
            return cmykImage;
        }

        public override ImageUInt8RGB ToByteRGBImage()
        {
            return (ImageUInt8RGB)Clone();
        }

        public override DataType GetDataType()
        {
            return DataType.UInt8;
        }
    }
}
