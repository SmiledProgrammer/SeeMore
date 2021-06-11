using System;

namespace SeeMore
{
    internal class ChannelUInt32 : GenericChannel<uint>
    {
        public ChannelUInt32(uint width, uint height) : base(width, height)
        { }

        public override Channel Clone()
        {
            ChannelUInt32 clone = new ChannelUInt32(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    clone[x, y] = Pixels[x, y];
                }
            }
            return clone;
        }

        public override GenericChannel<uint> Add(GenericChannel<uint> other)
        {
            ChannelUInt32 outcome = new ChannelUInt32(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    long newValue = (long)this[x, y] + other[x, y];
                    if (newValue > uint.MaxValue)
                    {
                        newValue = uint.MaxValue;
                    }
                    outcome[x, y] = (uint)newValue;
                }
            }
            return outcome;
        }

        public override void Multiply(double factor)
        {
            factor = Math.Abs(factor);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    double newValue = this[x, y] * factor;
                    if (newValue > uint.MaxValue)
                        newValue = uint.MaxValue;
                    this[x, y] = (uint)newValue;
                }
            }
        }

        public override byte[,] ToByteArray()
        {
            byte[,] array = new byte[Width, Height];
            long divider = ((long)uint.MaxValue + 1) / (byte.MaxValue + 1);
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
            return (double)Pixels[x, y] / uint.MaxValue * factor;
        }

        protected override uint ConvertFromDouble(double value)
        {
            return (uint)(Math.Abs(value) * uint.MaxValue);
        }
    }
}
