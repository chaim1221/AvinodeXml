using System;
using System.IO;
using System.Xml;

namespace AvinodeXmlParser
{
    public class Helper
    {
        public object Arg1;
        public object Arg2;
        public object XmlStuff;

        public void Validate(string[] args)
        {
            if (!File.Exists(args[0]))
                throw new ArgumentException(args[0]);
            if (!Uri.IsWellFormedUriString(args[1], UriKind.Relative))
                throw new ArgumentException(args[1]);
            Arg1 = args[0];
            Arg2 = args[1];
        }

        public void ParseXml(string validXml)
        {
            XmlStuff = validXml;
        }
    }
}