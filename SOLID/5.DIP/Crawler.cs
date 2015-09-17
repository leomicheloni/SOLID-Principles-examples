using System;
using System.Linq;
using System.Collections.Generic;

namespace SOLID.DIP
{
    public class Crawler
    {
        PageRetriever pageRetriever = new PageRetriever();

        private IImageRecorder fileManager;

        public IImageRecorder FileManager
        {
            get {
                if(this.fileManager == null)
                {
                    this.fileManager = new FileManager();

                }

                return fileManager;

            }
            set { fileManager = value; }
        }

        public void Process()
        {
            var doc = pageRetriever.GetPage();

            Console.WriteLine("crawling data");

            var body = doc.DocumentNode.ChildNodes.Single(node => node.Name == "html").ChildNodes.Single(node => node.Name == "body");

            var footer = body.ChildNodes.Single(node => node.Id == "main-footer");

            var containerFooter = footer.ChildNodes.Single(node => node.Name == "div");

            var imgs = containerFooter.ChildNodes["div"].ChildNodes.Where(node => node.Name == "img");

            var urls = new List<string>();

            foreach (var img in imgs)
            {
                urls.Add(img.Attributes["src"].Value);
            }

            foreach (var url in urls)
            {
                byte[] imageBytes = pageRetriever.GetImage(@"http://www.kinetica-solutions.com" + url);
                this.fileManager.SaveImage(imageBytes, "../../images/" + url.Split('/').Last());
            }

            Console.WriteLine("done!");

            Console.ReadLine();

        }
    }
}