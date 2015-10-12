using System;
using System.Collections.Generic;

namespace AvinodeXmlParser
{
    static class Program
    {
        static void Main(string[] args)
        {
            var helper = new Helper();
            helper.Validate(args);
            var model = helper.UnfurlNodes(helper.ParseXml());
            model.WriteValues();
        }

        private static void WriteValues(this List<AvinodeMenuItem> model, string modifier = null)
        {
            if (model == null) return;
            if (modifier == null) modifier = string.Empty;
            foreach (var item in model)
            {
                Console.WriteLine("{0}{1}, {2} {3}", modifier, item.DisplayName, item.Path, item.Active ? "ACTIVE" : string.Empty);
                WriteValues(item.SubMenuItem, modifier + "\t");
            }
        }
    }
}
