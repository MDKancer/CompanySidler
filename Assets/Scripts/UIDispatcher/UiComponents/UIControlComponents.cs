using System;
using System.Collections.Generic;
using System.Linq;
using GameSettings;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Action = Enums.Action;


namespace UIDispatcher.UiComponents
{
    [Serializable]
    public struct UIControlComponents
    {
        [HideLabel,HorizontalGroup("Controls")]
        public List<TextMeshProUGUI> action;
        [HideLabel,HorizontalGroup("Controls")]
        public List<TextMeshProUGUI> events;

        public void SetData(KeyboardsData[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                action[i].SetText(data[i].action);
                events[i].SetText(data[i].keyCode);
            }
        }
    }
}