namespace SeeMore
{
    public static class ImageFactory
    {
        public static Image Create(uint width, uint height, DataType type, ColorModel model)
        {
            switch (model)
            {
                case ColorModel.RGB:
                    switch (type)
                    {
                        case DataType.UInt8:  return new ImageUInt8RGB(width, height);
                        case DataType.UInt16: return new ImageUInt8RGB(width, height); //TODO
                        case DataType.UInt32: return new ImageUInt8RGB(width, height); //TODO
                        case DataType.Double: return new ImageUInt8RGB(width, height); //TODO
                    }
                    break;
                case ColorModel.HSV:
                    //TODO
                    break;
                case ColorModel.CMYK:
                    //TODO
                    break;
            }
            return null;
        }
    }
}
