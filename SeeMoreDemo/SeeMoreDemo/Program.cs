using SeeMore;

namespace SeeMoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/SeeMore/SeeMoreDemo/SeeMoreDemo/resources/lenna.png";
            string outputFile = "D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/SeeMore/SeeMoreDemo/SeeMoreDemo/resources/test.png";
            var image = ImageFactory.LoadFromFile(inputFile);
            var outcome = image.Filter(filter: FilterType.AVERAGE, KernelFactory.Neighborhood(KernelSize.SIZE_13x13));
            ImageFactory.SaveImageToFile(outputFile, outcome.ToByteRGBImage());
        }
    }
}
