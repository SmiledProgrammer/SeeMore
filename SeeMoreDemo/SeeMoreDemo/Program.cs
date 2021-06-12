using SeeMore;

namespace SeeMoreDemo
{
    class Program
    {
        static Image TestConvertions(Image image)
        {
            return image
                .ToRGB().ToDouble().ToUInt8().ToUInt16().ToDouble().ToUInt16().ToUInt32().ToDouble().ToDouble().ToUInt32().ToUInt32().ToUInt16().ToUInt16().ToUInt8().ToUInt8().ToUInt32().ToUInt8()
                .ToHSV().ToDouble().ToUInt8().ToUInt16().ToDouble().ToUInt16().ToUInt32().ToDouble().ToDouble().ToUInt32().ToUInt32().ToUInt16().ToUInt16().ToUInt8().ToUInt8().ToUInt32().ToUInt8()
                .ToCMYK().ToDouble().ToUInt8().ToUInt16().ToDouble().ToUInt16().ToUInt32().ToDouble().ToDouble().ToUInt32().ToUInt32().ToUInt16().ToUInt16().ToUInt8().ToUInt8().ToUInt32().ToUInt8()
                .ToRGB().ToHSV().ToRGB().ToCMYK().ToRGB()
                .ToUInt16().ToHSV().ToRGB().ToCMYK().ToRGB()
                .ToUInt32().ToHSV().ToRGB().ToCMYK().ToRGB()
                .ToDouble().ToHSV().ToRGB().ToCMYK().ToRGB();
        }

        static void Main(string[] args)
        {
            string inputFile = "D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/SeeMore/SeeMoreDemo/SeeMoreDemo/resources/lenna.png";
            string outputFile = "D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/Output/output.png";
            Image image = ImageFactory.LoadFromFile(inputFile);
            TestConvertions(image);
            Image outcome = image.Filter(kernel: KernelFactory.Neighborhood(KernelSize.SIZE_13x13), filter: FilterType.MEDIAN);
            ImageFactory.SaveImageToFile(outputFile, outcome);
        }
    }
}
