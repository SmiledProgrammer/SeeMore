namespace SeeMore
{
    public abstract class Image
    {
        protected uint Width, Height;

        protected Image(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public abstract Image Clone();
        public abstract Image Filter(FilterType filter, NeighborhoodSize neighborhoodSize, NeighborhoodType neighborhoodType);
        public abstract DataType GetDataType();
        public abstract ColorModel GetColorModel();
    }
}
