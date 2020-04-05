using System;
using System.IO;
using GameSettings;
using JetBrains.Annotations;
using UnityEngine;

namespace JSon_Manager
{
    public class Json_Stream
    {
        private string absolutePath;
        private string localPath;
        /// <summary>
        /// the json file will be in the asset folder created.
        /// </summary>
        /// <example>test.json</example>
        /// <param name="fileName">with this name</param>
        public Json_Stream(string fileName)
        {
            LocalPath = fileName;
        }

        public Json_Stream()
        {
            LocalPath = String.Empty;
            AbsolutePath = String.Empty;
        }

        /// <summary>
        /// Set the absolute path to the json file.
        /// <remarks>Set the path to the json file in the operating system.</remarks>
        /// <example>C:/Users/data.json</example>
        /// </summary>
        public string AbsolutePath
        {
            get => absolutePath;
            set => absolutePath = value;
        }
        /// <summary>
        /// Set the Local Path to the Json file.
        /// <remarks>Set the path to the json file in the Asset Folder</remarks>
        /// <example>Scripts/GameData/<param name="value"></param>.json</example>
        /// </summary>
        public string LocalPath
        {
            get => localPath;
            set
            {
                localPath = Application.dataPath + "/" + value;
                AbsolutePath = Path.GetFullPath(localPath);
            }
        }

        public void WriteJson<T>(T settingsData)
        {
            string jsonContext = null;
            
            
            jsonContext = JsonUtility.ToJson(settingsData);

            File.WriteAllText(absolutePath,jsonContext);
        }
        [CanBeNull]
        public T GetSettings<T>()
        {
            string json = String.Empty;

            if (!File.Exists(absolutePath))
            {
                WriteJson(new SettingsData());
            }
            
            json = File.ReadAllText(absolutePath);

            T settingsData = (T) JsonUtility.FromJson(json,typeof(T));
            return settingsData;
        }
    }
        
}
