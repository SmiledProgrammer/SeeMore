using SeeMore;

namespace SeeMoreDemo
{
    class Program
    {
        static Image ConvertToHSV(Image image)
        {
            return image.ToHSV();
        }

        static void Main(string[] args)
        {
            string inputFile = "D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/SeeMore/SeeMoreDemo/SeeMoreDemo/resources/lenna.png";
            string outputFile = "D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/Output/output.png";
            var image = ImageFactory.LoadFromFile(inputFile);
            var outcome = image;
            ConvertToHSV(image);
            ImageFactory.SaveImageToFile(outputFile, outcome);
        }
    }
}
