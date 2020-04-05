using System.IO;
using GameSettings;
using JetBrains.Annotations;
using UnityEngine;

namespace Json_Manager
{
    public class JsonStream
    {
        private string absolutePath;
        private string localPath;
        /// <summary>
        /// the json file will be in the asset folder created.
        /// </summary>
        /// <example>test.json</example>
        /// <param name="fileName">with this name</param>
        public JsonStream(string fileName)
        {
            LocalPath = fileName;
        }

        public JsonStream()
        {
            LocalPath =  Application.dataPath + "/";
        }

        /// <summary>
        /// Set the absolute path to the json file.
        /// <remarks>Set the path to the json file in the operating system.</remarks>
        /// <example>C:/Users/data.json</example>
        /// </summary>
        public string AbsolutePath
        {
            get => absolutePath;
            set
            {
                localPath = string.Empty;
                absolutePath = value;
            }
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
        /// <summary>
        /// Write a serializable object data in to a json file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void WriteJson<T>(T settingsData)
        {
            var jsonContext = JsonUtility.ToJson(settingsData);

            File.WriteAllText(AbsolutePath,jsonContext);
        }
        
        /// <summary>
        /// read the the json file and return that in a serializable <typeparam name="T"></typeparam> object.
        /// </summary>
        /// <typeparam name="T">Type of json file</typeparam>
        [CanBeNull]
        public T ReadJson<T>()
        {
            if (!File.Exists(absolutePath))
            {
                WriteJson(new SettingsData());
            }
            
            var json = File.ReadAllText(absolutePath);

            var settingsData = (T) JsonUtility.FromJson(json,typeof(T));
            return settingsData;
        }
    }
        
}
