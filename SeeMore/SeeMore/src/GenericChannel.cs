using System;

namespace SeeMore
{
    internal abstract class GenericChannel<T> : Channel
    {
        internal T[,] Pixels { get; set; }

        internal GenericChannel(uint width, uint height) : base (width, height)
        {
            Pixels = new T[width, height];
        }

        public abstract GenericChannel<T> Add(GenericChannel<T> other);
        internal abstract double GetMultipliedValue(uint x, uint y, double factor);
        protected abstract T ConvertFromDouble(double value);

        internal void Average(GenericChannel<T> originalChannel, GenericImage<T>.KernelFunction kernelFunction, uint x, uint y, uint kernelSize)
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

        internal void Median(GenericChannel<T> originalChannel, GenericImage<T>.KernelFunction kernelFunction, uint x, uint y, uint kernelSize)
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

        internal void Maximum(GenericChannel<T> originalChannel, GenericImage<T>.KernelFunction kernelFunction, uint x, uint y, uint kernelSize)
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

        internal void Minimum(GenericChannel<T> originalChannel, GenericImage<T>.KernelFunction kernelFunction, uint x, uint y, uint kernelSize)
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

        internal void Range(GenericChannel<T> originalChannel, GenericImage<T>.KernelFunction kernelFunction, uint x, uint y, uint kernelSize)
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
