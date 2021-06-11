using System;

namespace SeeMore
{
    internal class ChannelDouble : Channel<double>
    {
        public ChannelDouble(uint width, uint height) : base(width, height)
        { }

        public override Channel<double> Clone()
        {
            ChannelDouble clone = new ChannelDouble(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    clone[x, y] = Pixels[x, y];
                }
            }
            return clone;
        }

        public override Channel<double> Add(Channel<double> other)
        {
            ChannelDouble outcome = new ChannelDouble(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    double newValue = this[x, y] + other[x, y];
                    if (newValue > 1.0)
                    {
                        newValue = 1.0;
                    }
                    outcome[x, y] = newValue;
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
                    if (newValue > 1.0)
                    {
                        newValue = 1.0;
                    }
                    this[x, y] = newValue;
                }
            }
        }

        public override byte[,] ToByteArray()
        {
            byte[,] array = new byte[Width, Height];
            double divider = byte.MaxValue + 1;
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
            return Pixels[x, y] * factor;
        }

        protected override double ConvertFromDouble(double value)
        {
            return Math.Abs(value);
        }
    }
}
