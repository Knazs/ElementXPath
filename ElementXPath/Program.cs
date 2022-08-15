using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knazs.Xml.XPath
{
    internal class Program
    {
        public static void Main()
        {
            var @return = XElementNames.TryParse("asds:ASD:something", out var xElementNames);
            
            @return = XElementNames.TryParse("/", out xElementNames);

            @return = XElementNames.TryParse("/something", out xElementNames);
        }
    }
}
