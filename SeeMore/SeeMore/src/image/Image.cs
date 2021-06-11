namespace SeeMore
{
    public abstract class Image
    {
        public uint Width { get; }
        public uint Height { get; }

        protected Image(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public abstract Image Clone();

        public abstract Image ToRGB();
        public abstract Image ToHSV();
        public abstract Image ToCMYK();
        public abstract Image ToGray(GrayscaleConversionMethod method = GrayscaleConversionMethod.ARITHMETIC_MEAN);

        public abstract Image ToUInt8();
        public abstract Image ToUInt16();
        public abstract Image ToUInt32();
        public abstract Image ToDouble();

        public abstract Image Filter(Kernel kernel, FilterType filter = FilterType.AVERAGE, EdgeHandling edgeHandling = EdgeHandling.MIRROR_EXTENSION);
        public abstract Image Sobel();

        public abstract Channel GetChannel(ChannelType type);
        public abstract DataType GetDataType();
        public abstract ColorModel GetColorModel();
    }
}
