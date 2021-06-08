using System;

namespace SeeMore
{
    public class ChannelUInt8 : Channel<byte>
    {
        public ChannelUInt8(uint width, uint height) : base(width, height)
        { }

        public override Channel<byte> Clone()
        {
            ChannelUInt8 clone = new ChannelUInt8(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    clone[x, y] = Pixels[x, y];
                }
            }
            return clone;
        }

        public override Channel<byte> Add(Channel<byte> other)
        {
            ChannelUInt8 outcome = new ChannelUInt8(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    outcome[x, y] = (byte)(this[x, y] + other[x, y]);
                }
            }
            return outcome;
        }

        public override byte[,] ToByteArray()
        {
            byte[,] array = Pixels.Clone() as byte[,];
            return array;
        }

        public override double GetMultipliedValue(uint x, uint y, double factor)
        {
            return (double)Pixels[x, y] / byte.MaxValue * factor;
        }

        protected override byte ConvertFromDouble(double value)
        {
            return (byte)(Math.Abs(value) * byte.MaxValue);
        }
    }
}
