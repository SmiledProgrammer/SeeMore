using SeeMore;

namespace SeeMoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/SeeMore/SeeMoreDemo/SeeMoreDemo/resources/lenna.png";
            string outputFile = "D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/Output/output.png";
            var image = ImageFactory.LoadFromFile(inputFile);
            var outcome = image;
            outcome.R.Multiply(2.0);
            outcome.G.Multiply(2.0);
            ImageFactory.SaveImageToFile(outputFile, outcome.ToByteRGBImage());
        }
    }
}
