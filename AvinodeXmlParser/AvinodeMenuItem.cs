using System;

namespace AvinodeXmlParser
{
    public class AvinodeMenuItem
    {
        public string DisplayName { get; set; }
        public Uri Path { get; set; }
        public AvinodeMenuItem SubMenuItem { get; set; }
    }
}