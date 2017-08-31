using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    public class Logger
    {
        public Logger()
        {
            if (!Directory.Exists("Packets"))
            {
                Directory.CreateDirectory("Packets");
            }
        }

        public static void Write(string value, string name)
        {
            string path = $"Packets/{name}.txt";
            if (!File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                fs.Close();
                File.WriteAllText(path, value);
            }
            else
            {
                File.WriteAllText(path, value);
            }
        }
    }
}
