using System;

namespace SeeMore
{
    public abstract class Channel<T>
    {
        protected uint Width, Height;
        public T[,] Pixels { get; set; }

        public Channel(uint width, uint height)
        {
            Width = width;
            Height = height;
            Pixels = new T[width, height];
        }

        public abstract Channel<T> Clone();
        public abstract Channel<T> Add(Channel<T> other);
        public abstract void Multiply(double factor);
        public abstract byte[,] ToByteArray();
        internal abstract double GetMultipliedValue(uint x, uint y, double factor);
        protected abstract T ConvertFromDouble(double value);

        internal void Average(Channel<T> originalChannel, Image<T>.KernelFunction kernelFunction, uint x, uint y, uint kernelSize)
        {
            double sum = 0;
            Action<double> filterFunction = (p) =>
            {
                sum += p;
            };
            kernelFunction(originalChannel, x, y, filterFunction);
            T average = ConvertFromDouble(sum);
            Pixels[x, y] = average;
        }

        internal void Median(Channel<T> originalChannel, Image<T>.KernelFunction kernelFunction, uint x, uint y, uint kernelSize)
        {
            T[] pixels = new T[kernelSize * kernelSize];
            byte count = 0;
            Action<double> filterFunction = (p) =>
            {
                pixels[count] = ConvertFromDouble(p);
                count++;
            };
            kernelFunction(originalChannel, x, y, filterFunction);
            Array.Sort(pixels);
            T median = pixels[count / 2];
            Pixels[x, y] = median;
        }

        internal void Maximum(Channel<T> originalChannel, Image<T>.KernelFunction kernelFunction, uint x, uint y, uint kernelSize)
        {
            double max = 0.0;
            Action<double> filterFunction = (p) =>
            {
                if (p >= max)
                {
                    max = p;
                }
            };
            kernelFunction(originalChannel, x, y, filterFunction);
            Pixels[x, y] = ConvertFromDouble(max);
        }

        internal void Minimum(Channel<T> originalChannel, Image<T>.KernelFunction kernelFunction, uint x, uint y, uint kernelSize)
        {
            double min = double.MaxValue;
            Action<double> filterFunction = (p) =>
            {
                if (p <= min)
                {
                    min = p;
                }
            };
            kernelFunction(originalChannel, x, y, filterFunction);
            Pixels[x, y] = ConvertFromDouble(min);
        }

        internal void Range(Channel<T> originalChannel, Image<T>.KernelFunction kernelFunction, uint x, uint y, uint kernelSize)
        {
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
            kernelFunction(originalChannel, x, y, filterFunction);
            Pixels[x, y] = ConvertFromDouble(max - min);
        }

        public T this[int x, int y]
        {
            get => Pixels[x, y];
            set => Pixels[x, y] = value;
        }
    }
}
