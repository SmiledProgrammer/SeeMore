using System;
using SeeMore;

namespace SeeMoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Image outcome = image.Filter(filter: FilterType.MEDIAN, neighborhoodSize: NeighborhoodSize.SIZE_3x3, neighborhoodType: EdgeHandling.SKIP_UNDEFINED);
            */

            Image image = ImageFactory.LoadFromFile("D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/SeeMore/SeeMoreDemo/SeeMoreDemo/resources/dog.jpg");
            ImageUInt8RGB rgb = (ImageUInt8RGB)image;
            rgb.Print();

            Console.ReadLine();
        }
    }
}
