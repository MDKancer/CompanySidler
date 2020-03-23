using System;
using System.IO;
using GameSettings;
using JetBrains.Annotations;
using UnityEngine;

namespace JSon_Manager
{
    public class Json_Stream
    {
        public readonly string path ;
        public Json_Stream(string fileName)
        {
            path = Application.dataPath + "/" + fileName + ".json";
        }

        public void WriteJson(SettingsData settingsData)
        {
            string jsonContext = null;
            
            settingsData.DictionaryToJsonContext();
            
            jsonContext = JsonUtility.ToJson(settingsData);

            File.WriteAllText(path,jsonContext);
        }
        [CanBeNull]
        public SettingsData GetSettings()
        {
            string json = String.Empty;

            if (!File.Exists(path))
            {
                WriteJson(new SettingsData());
            }
            
            json = File.ReadAllText(path);

            SettingsData settingsData = JsonUtility.FromJson(json,typeof(SettingsData)) as SettingsData;
            settingsData.ArrayToDictionary();
            return settingsData;
        }
    }
        
}
