using Newtonsoft.Json;

namespace MooseEngine.Utilities
{
    public static class JsonUtility
    {
        public static T LoadFromJson<T>(string path) 
        {
            var txt = File.ReadAllText(path);
            var json = JsonConvert.DeserializeObject<T>(txt);
            return json!;
        }

        public static bool SaveToJson<T>(T obj,string path)
        {
            var json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(path, json);
            return File.Exists(path);
        }
    }
}
