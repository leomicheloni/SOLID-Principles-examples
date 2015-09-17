using System;

public class Crawler
{
	public Crawler()
	{



	}


    public void Process()
    {
        var req = (HttpWebRequest)WebRequest.Create(url);
        //req.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";

        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        HtmlDocument doc = new HtmlDocument();

        var resultStream = resp.GetResponseStream();
        doc.Load(resultStream);
        return doc;

    }
}
