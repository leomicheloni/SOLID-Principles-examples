using System;
using System.Linq;
using System.Collections.Generic;

namespace SOLID.LSP
{
    public class Crawler
    {
        RequestManager requestManager = new RequestManager();

        private FileManager fileManager;

        public FileManager FileManager
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

            //LSP !!!!!!!!
            if(this.fileManager.GetType() == typeof(DatabaseFileManager))
            {
                (this.fileManager as DatabaseFileManager).SetConnectionString("data source=./sqlexpress;user=sa;password=pepe");
            }

            var doc = requestManager.GetPage();

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
                byte[] imageBytes = requestManager.GetImage(@"http://www.kinetica-solutions.com" + url);
                this.fileManager.SaveImage(imageBytes, "../../images/" + url.Split('/').Last());
            }

            Console.WriteLine("done!");

            Console.ReadLine();

        }
    }
}