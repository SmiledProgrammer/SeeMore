using System;
using SeeMore;

namespace SeeMoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Image img = ImageFactory.Create(width: 5, height: 5, type: DataType.UInt8, model: ColorModel.RGB);
            ImageUInt8RGB rgbImg = (ImageUInt8RGB)img;
            rgbImg.GenerateSomeImage();
            rgbImg.Intensify();
            Image outcome = rgbImg.Filter(filter: FilterType.MEAN, neighborhoodSize: NeighborhoodSize.SIZE_5x5, neighborhoodType: NeighborhoodType.SKIP_UNDEFINED);

            Console.ReadLine();
        }
    }
}
