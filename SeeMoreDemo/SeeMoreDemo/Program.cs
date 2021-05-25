using SeeMore;

namespace SeeMoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/SeeMore/SeeMoreDemo/SeeMoreDemo/resources/dog.jpg";
            string outputFile = "D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/SeeMore/SeeMoreDemo/SeeMoreDemo/resources/test.png";
            var image = ImageFactory.LoadFromFile(inputFile);
            //var outcome = image.Filter(filter: FilterType.AVERAGE, neighborhoodSize: NeighborhoodSize.SIZE_9x9, edgeHandling: EdgeHandling.MIRROR_EXTENSION);
            var outcome = image.ToGray(GrayscaleConversionMethod.ARITHMETIC_MEAN);
            ImageFactory.SaveImageToFile(outputFile, outcome.ToByteRGBImage());
        }
    }
}
