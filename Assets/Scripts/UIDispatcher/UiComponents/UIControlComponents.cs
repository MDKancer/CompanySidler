using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Action = Enums.Action;


namespace UIDispatcher.GameComponents
{
    [Serializable]
    public struct UIControlComponents
    {
        [HideLabel,HorizontalGroup("Controls")]
        public List<TextMeshProUGUI> action;
        [HideLabel,HorizontalGroup("Controls")]
        public List<TextMeshProUGUI> events;

        public void SetDatas(Dictionary<Action, KeyCode> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                var item =  data.ElementAt(i);
                action[i].SetText(item.Key.ToString());
                events[i].SetText(item.Value.ToString());
            }
        }
    }
}