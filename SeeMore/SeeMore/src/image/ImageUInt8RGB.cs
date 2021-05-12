using System;

namespace SeeMore
{
    public class ImageUInt8RGB : ImageRGB<byte> // TODO: remove "public"
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
                    byte r = R.Pixels[x, y];
                    byte g = G.Pixels[x, y];
                    byte b = B.Pixels[x, y];
                    byte min = Math.Min(r, Math.Min(g, b));
                    byte max = Math.Max(r, Math.Max(g, b));
                    hsvImage.V.Pixels[x, y] = max;
                    if (max == 0)
                    {
                        hsvImage.S.Pixels[x, y] = 0;
                        hsvImage.H.Pixels[x, y] = 0;
                        break;
                    }
                    byte diff = (byte)(max - min);
                    byte saturation = (byte)(255 * diff / max);
                    hsvImage.S.Pixels[x, y] = saturation;
                    if (saturation == 0)
                    {
                        hsvImage.H.Pixels[x, y] = 0;
                        break;
                    }
                    if (max == r)
                        hsvImage.H.Pixels[x, y] = (byte)(43 * (g - b) / diff);
                    else if (max == g)
                        hsvImage.H.Pixels[x, y] = (byte)(85 + 43 * (b - r) / diff);
                    else
                        hsvImage.H.Pixels[x, y] = (byte)(171 + 43 * (r - g) / diff);
                }
            }
            return hsvImage;
        }

        public override DataType GetDataType()
        {
            return DataType.UInt8;
        }

        public void Print() //tmp
        {
            Console.WriteLine("R");
            R.Print();
            //Console.WriteLine("G");
            //G.Print();
            //Console.WriteLine("B");
            //B.Print();
        }

        public void GenerateSomeImage() //tmp
        {
            R.Randomize();
            G.Randomize();
            B.Randomize();
        }
    }
}
