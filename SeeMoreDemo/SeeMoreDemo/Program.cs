using SeeMore;

namespace SeeMoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Image image = ImageFactory.LoadFromFile("D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/SeeMore/SeeMoreDemo/SeeMoreDemo/resources/dog.jpg");
            Image outcome = image.Filter(filter: FilterType.AVERAGE, neighborhoodSize: NeighborhoodSize.SIZE_9x9, neighborhoodType: EdgeHandling.MIRROR_EXTENSION);
            ImageFactory.SaveImageToFile("D:/Pulpit/Pliki szkolne/Semestr 4/Projekt Indywidualny/SeeMore/SeeMoreDemo/SeeMoreDemo/resources/test.png", outcome);
        }
    }
}
