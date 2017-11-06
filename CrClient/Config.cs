using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrClient
{
    public class Config
    {
        public static Config Load()
        {
            var config = File.ReadAllText("Config.json");
            return JsonConvert.DeserializeObject<Config>(config);
        }
        public string ResourceHash;
        public string TagChars;
        public string ServerKey;
        public int MajorVersion;
        public int MinorVersion;
        public int BuildVersion;
        public bool UseRC4;
    }
}
