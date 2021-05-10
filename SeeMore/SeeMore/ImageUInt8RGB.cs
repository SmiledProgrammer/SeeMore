using System;

namespace SeeMore
{
    public class ImageUInt8RGB : ImageRGB<byte> // TODO: remove "public"
    {
        public ImageUInt8RGB(uint width, uint height) : base(width, height, new ChannelUInt8(width, height), new ChannelUInt8(width, height), new ChannelUInt8(width, height))
        {}

        public override void Average(Image originalImage, Action<byte[,], int, int, Action<byte>> neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageUInt8RGB castedOriginalImage = (ImageUInt8RGB)originalImage;
            ImageUInt8RGB castedOutputImage = (ImageUInt8RGB)outputImage;
            castedOutputImage.R.Average(castedOriginalImage.R, neighborhoodFunction, x, y);
            castedOutputImage.G.Average(castedOriginalImage.G, neighborhoodFunction, x, y);
            castedOutputImage.B.Average(castedOriginalImage.B, neighborhoodFunction, x, y);
        }

        public override void Median(Image originalImage, Action<byte[,], int, int, Action<byte>> neighborhoodFunction, uint neighborhoodSize, uint x, uint y, Image outputImage)
        {
            ImageUInt8RGB castedOriginalImage = (ImageUInt8RGB)originalImage;
            ImageUInt8RGB castedOutputImage = (ImageUInt8RGB)outputImage;
            castedOutputImage.R.Median(castedOriginalImage.R, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.G.Median(castedOriginalImage.G, neighborhoodFunction, x, y, neighborhoodSize);
            castedOutputImage.B.Median(castedOriginalImage.B, neighborhoodFunction, x, y, neighborhoodSize);
        }

        public override DataType GetDataType()
        {
            return DataType.UInt8;
        }


        public void Print() //tmp
        {
            Console.WriteLine("R");
            R.Print();
            //Console.WriteLine("G");
            //G.Print();
            //Console.WriteLine("B");
            //B.Print();
        }

        public void GenerateSomeImage() //tmp
        {
            R.Randomize();
            G.Randomize();
            B.Randomize();
        }
    }
}
