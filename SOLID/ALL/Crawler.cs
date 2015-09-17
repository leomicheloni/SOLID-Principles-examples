using System;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace SOLID.ALL
{
    public class Crawler
    {
        private IImageRecorder fileManager;
        private ILogger Logger;
        private IPageRetriever pageRetriever;

        public IPageRetriever PageRetriever
        {
            get
            {
                if(this.pageRetriever == null)
                {
                    this.pageRetriever = ServiceLocator.Find<IPageRetriever>();
                }
                return pageRetriever;
            }
            set { pageRetriever = value; }
        }

        public ILogger MyProperty
        {
            get
            {
                if(this.Logger == null)
                {
                    this.Logger = ServiceLocator.Find<ILogger>();
                }
                return Logger;
            }
            set { Logger = value; }
        }

        public IImageRecorder FileManager
        {
            get
            {
                if(this.fileManager == null)
                {
                    this.fileManager = ServiceLocator.Find<IImageRecorder>();
                }

                return fileManager;

            }
            set { fileManager = value; }
        }

        public void Process(string baseUrl)
        {
            var doc = pageRetriever.GetPage(baseUrl);

            Logger.Log("crawling data");

            var body = this.GetBody(doc);
            var footer = this.GetFooter(body);
            var containerFooter = footer.ChildNodes.Single(node => node.Name == "div");
            var imgs = this.GetImagesUrls(containerFooter);

            foreach (var url in imgs)
            {
                var imageBytes = pageRetriever.GetImage($"{baseUrl}{url}");
                fileManager.SaveImage(imageBytes, $"../../images/{url.Split('/').Last()}");
            }

            Logger.Log("done!");
        }

        private IEnumerable<string> GetImagesUrls(HtmlNode containerFooter)
        {
            return containerFooter.ChildNodes["div"].ChildNodes.Where(node => node.Name == "img")
                            .Select(img => img.Attributes["src"].Value);
        }

        private HtmlNode GetFooter(HtmlNode body)
        {
            return body.ChildNodes.Single(node => node.Id == "main-footer");
        }

        private HtmlNode GetBody(HtmlAgilityPack.HtmlDocument doc)
        {
            return doc.DocumentNode.ChildNodes.Single(node => node.Name == "html").ChildNodes.Single(node => node.Name == "body");
        }
    }
}