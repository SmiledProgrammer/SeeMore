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

        public override byte[,] ToByteArray() // TODO: remove unnecessary code l8r
        {
            /*byte[,] array = new byte[Width, Height];
            for (uint x = 0; x < Width; x++)
            {
                for (uint y = 0; y < Height; y++)
                {
                    array[x, y] = Pixels[x, y];
                }
            }*/
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

        public override void Average(Channel<byte> originalChannel, Image<byte>.KernelFunction kernelFunction, uint x, uint y, uint neighborhoodSize)
        {
            ChannelUInt8 castedOriginalChannel = (ChannelUInt8)originalChannel;
            double sum = 0;
            byte count = 0;
            Action<double> filterFunction = (p) =>
            {
                sum += p;
                count++;
            };
            kernelFunction(castedOriginalChannel, x, y, filterFunction);
            byte average = ConvertFromDouble(sum / count);
            Pixels[x, y] = average;
        }

        public override void Median(Channel<byte> originalChannel, Image<byte>.KernelFunction kernelFunction, uint x, uint y, uint neighborhoodSize)
        {
            ChannelUInt8 castedOriginalChannel = (ChannelUInt8)originalChannel;
            byte[] pixels = new byte[neighborhoodSize * neighborhoodSize];
            byte count = 0;
            Action<double> filterFunction = (p) =>
            {
                pixels[count] = ConvertFromDouble(p);
                count++;
            };
            kernelFunction(castedOriginalChannel, x, y, filterFunction);
            Array.Sort(pixels);
            byte median = pixels[count / 2];
            Pixels[x, y] = median;
        }

        public override void Maximum(Channel<byte> originalChannel, Image<byte>.KernelFunction kernelFunction, uint x, uint y, uint neighborhoodSize)
        {
            ChannelUInt8 castedOriginalChannel = (ChannelUInt8)originalChannel;
            double max = 0.0;
            Action<double> filterFunction = (p) =>
            {
                if (p >= max)
                {
                    max = p;
                }
            };
            kernelFunction(castedOriginalChannel, x, y, filterFunction);
            Pixels[x, y] = ConvertFromDouble(max);
        }

        public override void Minimum(Channel<byte> originalChannel, Image<byte>.KernelFunction kernelFunction, uint x, uint y, uint neighborhoodSize)
        {
            ChannelUInt8 castedOriginalChannel = (ChannelUInt8)originalChannel;
            double min = double.MaxValue;
            Action<double> filterFunction = (p) =>
            {
                if (p <= min)
                {
                    min = p;
                }
            };
            kernelFunction(castedOriginalChannel, x, y, filterFunction);
            Pixels[x, y] = ConvertFromDouble(min);
        }

        public override void Range(Channel<byte> originalChannel, Image<byte>.KernelFunction kernelFunction, uint x, uint y, uint neighborhoodSize)
        {
            ChannelUInt8 castedOriginalChannel = (ChannelUInt8)originalChannel;
            double max = 0.0;
            double min = double.MaxValue;
            Action<double> filterFunction = (p) =>
            {
                if (p >= max)
                {
                    max = p;
                }
                if (p <= min)
                {
                    min = p;
                }
            };
            kernelFunction(castedOriginalChannel, x, y, filterFunction);
            Pixels[x, y] = (byte)(max - min);
        }
    }
}
