using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ALL
{
    class PageRetriever : IPageRetriever
    {
        private ILogger Logger;
        public ILogger MyProperty
        {
            get
            {
                if (this.Logger == null)
                {
                    this.Logger = ServiceLocator.Find<ILogger>();
                }
                return Logger;
            }
            set { Logger = value; }
        }

        public HtmlDocument GetPage(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            HtmlDocument doc = new HtmlDocument();

            var resultStream = resp.GetResponseStream();
            doc.Load(resultStream);

            return doc;
        }

        public byte[] GetImage(string url)
        {
            byte[] imageBytes;
            HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(url);
            WebResponse imageResponse = imageRequest.GetResponse();

            Logger.Log("downloading image");

            var responseStream = imageResponse.GetResponseStream();

            using (var binaryReader = new BinaryReader(responseStream))
            {
                imageBytes = binaryReader.ReadBytes((int)imageResponse.ContentLength);
                binaryReader.Close();
            }

            responseStream.Close();
            imageResponse.Close();

            return imageBytes;
        }
    }
}
