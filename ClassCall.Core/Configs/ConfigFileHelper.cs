using Newtonsoft.Json;
using System.IO;

namespace ClassCall.Core.Configs
{
    public static class ConfigFileHelper<T>
    {
        public static bool Save(string path, T config)
        {
            try
            {
                var json = JsonConvert.SerializeObject(config);
                File.WriteAllText(path, json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Load(string path, ref T config)
        {
            if (!File.Exists(path))
                return false;
            try
            {
                var json = File.ReadAllText(path);
                config = JsonConvert.DeserializeObject<T>(json);
                if (config == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
