using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Action = Enums.Action;

namespace GameSettings
{
    [Serializable]
    public class SettingsData
    {

        public AudioData audioData;
        public VideoData videoData;
        [NonSerialized]
        public Dictionary<Action, KeyCode> keyboardData;
        [SerializeField]
        private KeyboardsData[] inputBindings;

        public SettingsData()
        {
            audioData = new AudioData();
            videoData = new VideoData();
            keyboardData = DefaultKeyBoardData();
        }
        /// <summary>
        /// use this Method to convert the dictionarys value to array
        /// only when you will to create a json file 
        /// <remarks>the dictionary will be converted into json context and saved it into <paramref name="inputListenners"/></remarks>
        /// </summary>
        public void DictionaryToJsonContext()
        {
            inputBindings = new KeyboardsData[keyboardData.Count];

            for (int i = 0; i < keyboardData.Count; i++)
            {
                var item =  this.keyboardData.ElementAt(i);
                var keyboardData = new KeyboardsData
                {
                    action = item.Key.ToString(),
                    keyCode = item.Value.ToString()
                };
                inputBindings[i] = keyboardData;
            }
        }
        /// <summary>
        /// use this Method to convert from  array values to the dictionary
        /// only when you will to read a json file 
        /// <remarks>the array will be converted from json context and saved it into dictionary <paramref name="jsonContext"/></remarks>
        /// </summary>
        public void ArrayToDictionary()
        {
            foreach (var data in inputBindings)
            {
                Action action = (Action) Enum.Parse(typeof(Action), data.action);
                KeyCode keyCode = (KeyCode) Enum.Parse(typeof(KeyCode), data.keyCode);
                keyboardData[action] = keyCode;
            }
        }

        public void Reset()
        {
            audioData = new AudioData();
            videoData = new VideoData();

            keyboardData = DefaultKeyBoardData();
        }

        private Dictionary<Action, KeyCode> DefaultKeyBoardData()
        {
            return new Dictionary<Action,KeyCode>
            {
                {Action.MOVE_FORWARD,KeyCode.W},
                {Action.MOVE_BACK,KeyCode.S},
                {Action.MOVE_LEFT,KeyCode.A},
                {Action.MOVE_RIGHT,KeyCode.D},
                {Action.ROTATE_RIGHT,KeyCode.E},
                {Action.ROTATE_LEFT,KeyCode.Q},
                {Action.FOCUS_ON, KeyCode.Mouse0},
                {Action.RETURN,KeyCode.Escape},
                {Action.MENU,KeyCode.M}
            };
        }
    }
}