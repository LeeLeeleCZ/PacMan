using Newtonsoft.Json;
using System.Data;
using System.Data.SQLite;

namespace PAC_MAN
{
    public delegate void SettingsUpdate();
    internal static class Nastavení
    {
        public static event SettingsUpdate SettingsUpdate;
        public static SQLiteConnection m_dbConnection;
        public static bool hudba = true;
        public static bool zvuk = true;
        public static bool controls = true;
        public static AutoScaleMode ScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        

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

        #region SQL
        public static void ZkontrolujDatabazi()
        {
            // https://web.archive.org/web/20190910153157/http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
            // https://inloop.github.io/sqlite-viewer/
            m_dbConnection = new SQLiteConnection("Data Source=../../../Score/Score.sqlite; version=3;");
            m_dbConnection.Open();
            SQLiteCommand command;

            if (!tableAlreadyExists(m_dbConnection, "default"))
            {
                command = new SQLiteCommand(m_dbConnection);
                command.CommandText = @"CREATE TABLE 'default' (name VARCHAR(20), score INT, cas INT, zivoty INT)"; //(name, score, cas, zivoty)
                command.ExecuteNonQuery();
            }
        }

        public static bool tableAlreadyExists(SQLiteConnection openConnection, string tableName)
        {
            var sql =
                "SELECT name FROM sqlite_master WHERE type='table' AND name='" + tableName + "';";
            if (openConnection.State == System.Data.ConnectionState.Open)
            {
                SQLiteCommand command = new SQLiteCommand(sql, openConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                return false;
            }
            else
            {
                throw new System.ArgumentException("Data.ConnectionState must be open");
            }
        }

        public static void NaplnDataset(DataSet ds, string query)
        {
            var conn = Nastavení.m_dbConnection;
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))// error conn 
            {
                using (var DataAdapterd = new SQLiteDataAdapter(cmd))
                {
                    ds.Clear();
                    DataAdapterd.Fill(ds);
                }
            }
        }
        #endregion
    }
}
