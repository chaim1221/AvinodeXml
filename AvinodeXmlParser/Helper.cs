using System;
using System.IO;

namespace AvinodeXmlParser
{
    public class Helper
    {
        public object Arg1;
        public object Arg2;

        public void Validate(string[] args)
        {
            if (!File.Exists(args[0]))
                throw new ArgumentException(args[0]);
            Arg1 = args[0];
            Arg2 = args[1];
        }
    }
}