using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        [SerializeField]
        public KeyboardsData[] inputBindings;

        public SettingsData()
        {
            audioData = new AudioData();
            videoData = new VideoData();
            inputBindings = DefaultKeyBoardData();
        }
        public void Reset()
        {
            audioData = new AudioData();
            videoData = new VideoData();

            inputBindings = DefaultKeyBoardData();
        }
        
        private KeyboardsData[] DefaultKeyBoardData()
        {
            return new []
            {
                new KeyboardsData{ action = Action.MOVE_FORWARD.ToString(),keyCode = KeyCode.W.ToString()},
                new KeyboardsData{ action = Action.MOVE_BACK.ToString(),keyCode = KeyCode.S.ToString()},
                new KeyboardsData{ action = Action.MOVE_LEFT.ToString(),keyCode = KeyCode.A.ToString()},
                new KeyboardsData{ action = Action.MOVE_RIGHT.ToString(),keyCode = KeyCode.D.ToString()},
                new KeyboardsData{ action = Action.ROTATE_RIGHT.ToString(),keyCode = KeyCode.E.ToString()},
                new KeyboardsData{ action = Action.ROTATE_LEFT.ToString(),keyCode = KeyCode.Q.ToString()},
                new KeyboardsData{ action = Action.FOCUS_ON.ToString(),keyCode = KeyCode.Mouse0.ToString()},
                new KeyboardsData{ action = Action.RETURN.ToString(),keyCode = KeyCode.Escape.ToString()},
                new KeyboardsData{ action = Action.MENU.ToString(),keyCode = KeyCode.M.ToString()},
            };
        }
    }
}