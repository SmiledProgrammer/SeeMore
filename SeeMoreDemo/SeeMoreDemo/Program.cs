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
            var outcome = image.Filter(filter: FilterType.AVERAGE, kernel: KernelFactory.Neighborhood(KernelSize.SIZE_13x13), edgeHandling: EdgeHandling.MIRROR_EXTENSION);
            ImageFactory.SaveImageToFile(outputFile, outcome.ToByteRGBImage());
        }
    }
}
