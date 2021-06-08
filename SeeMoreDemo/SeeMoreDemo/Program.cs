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
            var outcome = image.ToRGB().ToUInt16().ToUInt8().ToHSV().ToUInt16().ToUInt8().ToCMYK().ToUInt16().ToUInt8().ToGray(GrayscaleConversionMethod.HARMONIC_MEAN);
            //outcome.R.Multiply(2.0);
            //outcome.G.Multiply(2.0);
            //outcome.B.Multiply(2.0);
            ImageFactory.SaveImageToFile(outputFile, outcome.ToByteRGBImage());
        }
    }
}
