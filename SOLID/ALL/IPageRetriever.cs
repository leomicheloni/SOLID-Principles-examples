using HtmlAgilityPack;

namespace SOLID.ALL
{
    public interface IPageRetriever
    {
        byte[] GetImage(string url);

        HtmlDocument GetPage(string baseUrl);
    }
}