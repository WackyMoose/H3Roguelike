using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MooseEngine.Utilities
{
    public static class JsonUtility
    {
        public static T LoadFromJson<T>(string path) 
        {
            var json = JsonConvert.DeserializeObject<T>(path);
            return json!;
        }
    }
}
