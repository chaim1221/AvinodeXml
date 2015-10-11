using System;
using System.Collections.Generic;

namespace AvinodeXmlParser
{
    public class AvinodeMenuItem
    {
        public string DisplayName { get; set; }
        public Uri Path { get; set; }
        public bool Active { get ; set; }
        public List<AvinodeMenuItem> SubMenuItem { get; set; }
    }
}