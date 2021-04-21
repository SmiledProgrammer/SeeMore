using System;

namespace SeeMore
{
    public class ImageUInt8RGB : ImageRGB<byte> // TODO: remove "public"
    {
        public ImageUInt8RGB(uint width, uint height) : base(width, height, new ChannelUInt8(width, height), new ChannelUInt8(width, height), new ChannelUInt8(width, height))
        {}

        public override Image Intensify() //tmp
        {
            ImageUInt8RGB result = new ImageUInt8RGB(Width, Height);
            Func<byte[,], int, int, byte> operation = (p, x, y) => (byte)(p[x,y] * 3 / 2);
            result.ApplyToChannels(operation);
            return result;
        }

        public void Print() //tmp
        {
            R.Print();
            G.Print();
            B.Print();
        }

        public void GenerateSomeImage() //tmp
        {
            R.Randomize();
            G.Randomize();
            B.Randomize();
        }
    }
}
