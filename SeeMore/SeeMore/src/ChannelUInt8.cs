using System;

namespace SeeMore
{
    internal class ChannelUInt8 : Channel<byte>
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
                    int newValue = this[x, y] + other[x, y];
                    if (newValue > byte.MaxValue)
                    {
                        newValue = byte.MaxValue;
                    }
                    outcome[x, y] = (byte)newValue;
                }
            }
            return outcome;
        }

        public override void Multiply(double factor)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    double newValue = this[x, y] * factor;
                    if (newValue > byte.MaxValue)
                    {
                        newValue = byte.MaxValue;
                    }
                    this[x, y] = (byte)newValue;
                }
            }
        }

        public override byte[,] ToByteArray()
        {
            byte[,] array = Pixels.Clone() as byte[,];
            return array;
        }

        internal override double GetMultipliedValue(uint x, uint y, double factor)
        {
            return (double)Pixels[x, y] / byte.MaxValue * factor;
        }

        protected override byte ConvertFromDouble(double value)
        {
            return (byte)(Math.Abs(value) * byte.MaxValue);
        }
    }
}
