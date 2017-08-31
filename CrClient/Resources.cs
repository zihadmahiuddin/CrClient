using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    class Resources
    {
        public static Definition Definition;
        public static Logger Logger;

        public Resources()
        {
            Definition = new Definition();

            Logger = new Logger();

        }
    }
}
