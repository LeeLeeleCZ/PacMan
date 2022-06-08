using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PAC_MAN
{
    public delegate void SettingsUpdate();
    internal static class Nastavení
    {
        public static event SettingsUpdate SettingsUpdate;
        
        public static bool hudba = true;
        public static bool zvuk = true;
        public static bool controls = true;

        public static bool Hudba
        {
            get { return hudba; }
            set {
                hudba = value;
                SettingsUpdate();
                SaveSettings();
            }
        }

        public static bool Zvuk
        {
            get { return zvuk; }
            set
            {
                zvuk = value;
                SettingsUpdate();
                SaveSettings();
            }
        }

        public static bool Controls
        {
            get { return controls; }
            set
            {
                controls = value;
                SettingsUpdate();
                SaveSettings();
            }
        }

        public static void SaveSettings()
        {
            using (StreamWriter sw = new StreamWriter("../../../Nastavení/Nastavení.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, new Settings(hudba, zvuk, controls));
                }
            }
        }

        public static void LoadSettings()
        {
            if (File.Exists("../../../Nastavení/Nastavení.json"))
            {
                string json = File.ReadAllText("../../../Nastavení/Nastavení.json");
                Settings _Nastavení = JsonConvert.DeserializeObject<Settings>(json);
                controls = _Nastavení.controls;
                hudba = _Nastavení.hudba;
                zvuk = _Nastavení.zvuk;
            }
        }

        public class Settings
        {
            public bool hudba;
            public bool zvuk;
            public bool controls;

            public Settings(bool hudba, bool zvuk, bool controls)
            {
                this.hudba = hudba;
                this.zvuk = zvuk;
                this.controls = controls;
            }
        }
    }
}
