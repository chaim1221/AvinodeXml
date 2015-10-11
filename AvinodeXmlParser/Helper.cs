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
        public Uri RelativeUri;

        public void Validate(string[] args)
        {
            if (!File.Exists(args[0]))
                throw new ArgumentException(args[0]);
            if (!Uri.IsWellFormedUriString(args[1], UriKind.Relative))
                throw new ArgumentException(args[1]);
            FilePath = args[0];
            RelativeUri = new Uri(args[1], UriKind.Relative);
        }

        public XmlNodeList ParseXml()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(FilePath);
            return xmlDocument.SelectNodes("menu/item");
        }

        public List<AvinodeMenuItem> UnfurlNodes(XmlNodeList xmlNodes)
        {
            var avinodeMenuItems = new List<AvinodeMenuItem>();
            foreach (XmlNode node in xmlNodes)
            {
                var displayName = node["displayName"];
                var nodePath = node["path"];
                var subMenu = node.SelectNodes("subMenu/item");

                if (displayName != null && nodePath != null)
                {
                    var uriPath = new Uri(nodePath.Attributes["value"].Value, UriKind.Relative);
                    var subMenuItem = subMenu != null && subMenu.Count > 0 ? UnfurlNodes(subMenu) : null;
                    var isActive = RelativeUri == uriPath || (subMenuItem != null && subMenuItem.Any(menuItem => menuItem.Active));

                    avinodeMenuItems.Add(new AvinodeMenuItem
                    {
                        DisplayName = displayName.InnerText,
                        Path = uriPath,
                        Active = isActive,
                        SubMenuItem = subMenuItem
                    });
                }
            }
            return avinodeMenuItems;
        }
    }
}
