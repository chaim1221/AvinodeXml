using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace AvinodeXmlParser
{
    public class Helper
    {
        public string FilePath;
        public string RelativeUri;
        public XmlDocument XmlDocument;
        public XmlNodeList XmlNodeList;
        public AvinodeMenuItem AvinodeMenuItem;

        public void Validate(string[] args)
        {
            if (!File.Exists(args[0]))
                throw new ArgumentException(args[0]);
            if (!Uri.IsWellFormedUriString(args[1], UriKind.Relative))
                throw new ArgumentException(args[1]);
            FilePath = args[0];
            RelativeUri = args[1];
        }

        public void ParseXml()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(FilePath);
            XmlDocument = xmlDocument;
            XmlNodeList = xmlDocument.SelectNodes("menu/item");
            AvinodeMenuItem = new AvinodeMenuItem();
            if (XmlNodeList == null) return;
            foreach (XmlNode node in XmlNodeList)
            {
                var displayName = node["displayName"];
                if (displayName != null) AvinodeMenuItem.DisplayName = displayName.InnerText;
                var path = node["path"];
                if (path != null) AvinodeMenuItem.Path = new Uri(path.Attributes["value"].Value, UriKind.Relative);
                break;
            }
        }
    }
}
