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
        public List<AvinodeMenuItem> AvinodeMenuItems;

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
            AvinodeMenuItems = new List<AvinodeMenuItem>();
            if (XmlNodeList == null) return;
            AvinodeMenuItems = UnfurlNodes(XmlNodeList);
        }

        private List<AvinodeMenuItem> UnfurlNodes(XmlNodeList xmlNodes)
        {
            var i = 0;
            var avinodeMenuItems = new List<AvinodeMenuItem>();
            foreach (XmlNode node in xmlNodes)
            {
                var displayName = node["displayName"];
                var path = node["path"];
                var subMenu = node.SelectNodes("subMenu/item");

                if (displayName != null && path != null)
                {
                    avinodeMenuItems.Add(new AvinodeMenuItem
                    {
                        DisplayName = displayName.InnerText,
                        Path = new Uri(path.Attributes["value"].Value, UriKind.Relative),
                        SubMenuItem = subMenu != null && subMenu.Count > 0 ? UnfurlNodes(subMenu) : null
                    });
                }
                i++;

                if (i == 3) break;
            }
            return avinodeMenuItems;
        }
    }
}
