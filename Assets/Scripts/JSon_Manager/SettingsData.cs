using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Action = Enums.Action;

namespace JSon_Manager
{
    [Serializable]
    public class SettingsData
    {

        public AudioData audioData;
        public VideoData videoData;
        public Dictionary<Action,KeyCode> inputListenners = new Dictionary<Action,KeyCode>
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
        public KeyboardsData[] inputBindings;

        public SettingsData()
        {
            DictionaryToJsonContext();
        }
        /// <summary>
        /// the dictionary will be converted into json context and saved it into <paramref name="jsonContext"/>
        /// </summary>
        public void DictionaryToJsonContext()
        {
            inputBindings = new KeyboardsData[inputListenners.Count];

            for (int i = 0; i < inputListenners.Count; i++)
            {
                var item =  inputListenners.ElementAt(i);
                var keyboardData = new KeyboardsData
                {
                    action = item.Key.ToString(),
                    keyCode = item.Value.GetHashCode()
                };
                inputBindings[i] = keyboardData;
            }
        }
    }
}