using System;

namespace AvinodeXmlParser
{
    public class Helper
    {
        public object Arg1;
        public object Arg2;

        public void Validate(string[] args)
        {
            Arg1 = args[0];
            Arg2 = args[1];
            throw new ArgumentException(args[0]);
        }
    }
}