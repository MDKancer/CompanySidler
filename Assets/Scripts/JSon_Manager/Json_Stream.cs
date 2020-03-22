using System.IO;
using UnityEngine;

namespace JSon_Manager
{
    public class Json_Stream
    {
        private string path ;
        public Json_Stream(string fileName)
        {
            path = Application.dataPath + "/" + fileName + ".json";
        }

        public void WriteJson()
        {
            string jsonContext = null;
            var settingsData = new SettingsData();
            
            jsonContext = JsonUtility.ToJson(settingsData);

            File.WriteAllText(path,jsonContext);
        }

        public SettingsData GetSettings()
        {
            SettingsData settingsData = JsonUtility.FromJson<SettingsData>(path);
            return settingsData;
        }
    }
        
}
