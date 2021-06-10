using System;

namespace SeeMore
{
    public class ChannelUInt16 : Channel<ushort>
    {
        public ChannelUInt16(uint width, uint height) : base(width, height)
        { }

        public override Channel<ushort> Clone()
        {
            ChannelUInt16 clone = new ChannelUInt16(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    clone[x, y] = Pixels[x, y];
                }
            }
            return clone;
        }

        public override Channel<ushort> Add(Channel<ushort> other)
        {
            ChannelUInt16 outcome = new ChannelUInt16(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int newValue = this[x, y] + other[x, y];
                    if (newValue > ushort.MaxValue)
                    {
                        newValue = ushort.MaxValue;
                    }
                    outcome[x, y] = (ushort)newValue;
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
                    if (newValue > ushort.MaxValue)
                    {
                        newValue = ushort.MaxValue;
                    }
                    this[x, y] = (ushort)newValue;
                }
            }
        }

        public override byte[,] ToByteArray()
        {
            byte[,] array = new byte[Width, Height];
            int divider = (ushort.MaxValue + 1) / (byte.MaxValue + 1);
            for (uint x = 0; x < Width; x++)
            {
                for (uint y = 0; y < Height; y++)
                {
                    array[x, y] = (byte)(Pixels[x, y] / divider);
                }
            }
            return array;
        }

        internal override double GetMultipliedValue(uint x, uint y, double factor)
        {
            return (double)Pixels[x, y] / ushort.MaxValue * factor;
        }

        protected override ushort ConvertFromDouble(double value)
        {
            return (ushort)(Math.Abs(value) * ushort.MaxValue);
        }
    }
}
