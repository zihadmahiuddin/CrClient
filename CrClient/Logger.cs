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
            try
            {
                if (!Directory.Exists("Packets"))
                {
                    Directory.CreateDirectory("Packets");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error has occured while initializing the logger.\nError: {ex.Message}");
            }
        }

        public static void Write(string value, string name)
        {
            string path = $"Packets/{name}.bin";
            try
            {
                File.WriteAllText(path, value);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error has occured while initializing the logger.\nError: {ex.Message}");
            }
        }
    }
}
