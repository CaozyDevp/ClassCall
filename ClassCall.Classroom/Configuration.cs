using ClassCall.Core.Configs;
using System.IO;

namespace ClassCall.Classroom
{
    internal static class Configuration
    {
        public static ClassroomConfig Config
        {
            get => _config;
            private set => _config = value;
        }
        private static ClassroomConfig _config;

        public static KeyManager KeyManager
        {
            get => _keyManager;
            private set => _keyManager = value;
        }
        private static KeyManager _keyManager;

        public static string ConfigFilePath => "config.json";

        public static string KeyFilePath => "pub_key.xml";

        public static bool SaveKey()
        {
            if (KeyManager == null)
                return false;
            try
            {
                File.WriteAllText(KeyFilePath, KeyManager.GetXmlString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool LoadKey()
        {
            try
            {
                KeyManager = new KeyManager();
                string text = File.ReadAllText(KeyFilePath);
                return KeyManager.FromXmlString(text, false);
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveConfig()
        {
            return ConfigFileHelper<ClassroomConfig>.Save(ConfigFilePath, Config);
        }

        public static bool LoadConfig()
        {
            return ConfigFileHelper<ClassroomConfig>.Load(ConfigFilePath, ref _config);
        }

        public static void ResetConfig() => Config = new ClassroomConfig();
    }
}
