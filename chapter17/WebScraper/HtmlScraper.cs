using System;
using System.IO;
using System.Text;
using System.Xml;
using HtmlAgilityPack;


public sealed class HtmlScraper
{
    public static XmlDocument GetHtmlAsXml()
    {
        //Set up an in-memory stream to hold the HTML.
        MemoryStream stream = new MemoryStream();
        XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);

        //Grab HTML over the web and convert to XML.
        HtmlWeb web = new HtmlWeb();
        web.LoadHtmlAsXml("http://haacked.com/Demos/screen.html", writer);

        //Now read from that in-memory stream 
        //into a new XmlDocument class.
        XmlDocument xml = LoadFromStream(stream);
        return xml;
    }

    private static XmlDocument LoadFromStream(Stream stream)
    {
        XmlDocument xml = new XmlDocument();
        stream.Position = 0;
        XmlReader reader = XmlReader.Create(stream);
        xml.Load(reader);
        return xml;
    }
}

