using System;
using SeeMore;

namespace SeeMoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageUInt8RGB image = (ImageUInt8RGB)ImageFactory.Create(width: 5, height: 5, type: DataType.UInt8, model: ColorModel.RGB);
            image.GenerateSomeImage();
            ImageUInt8RGB outcome = (ImageUInt8RGB)image.Filter(filter: FilterType.MEDIAN, neighborhoodSize: NeighborhoodSize.SIZE_3x3, neighborhoodType: NeighborhoodType.SKIP_UNDEFINED);

            image.Print();
            Console.WriteLine();
            outcome.Print();

            Console.ReadLine();
        }
    }
}
