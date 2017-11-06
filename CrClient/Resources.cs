using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    class Resources
    {
        public static Logger Logger;
        public static ConfigManager ConfigManager;

        public Resources()
        {

            Logger = new Logger();
            ConfigManager = new ConfigManager();

        }
    }
}
