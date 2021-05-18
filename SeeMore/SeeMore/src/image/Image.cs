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
        public abstract ImageUInt8RGB ToByteRGBImage();
        public abstract Image Filter(FilterType filter, NeighborhoodSize neighborhoodSize, EdgeHandling neighborhoodType);
        public abstract DataType GetDataType();
        public abstract ColorModel GetColorModel();
    }
}
