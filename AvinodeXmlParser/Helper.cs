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
            XmlNodeList = xmlDocument.GetElementsByTagName("item");
        }
    }
}
