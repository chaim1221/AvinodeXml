using System;

namespace AvinodeXmlParser
{
    public class Helper
    {
        public object Arg1;
        public object Arg2;

        public void Validate(string[] args)
        {
            if (args[0] == "arg1")
                throw new ArgumentException(args[0]);
            Arg1 = args[0];
            Arg2 = args[1];
        }
    }
}