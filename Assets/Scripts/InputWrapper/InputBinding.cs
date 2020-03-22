using System.Collections.Generic;
using Container;
using Enums;
using JSon_Manager;
using UnityEngine;
using Zenject;

namespace InputWrapper
{
    public class InputBinding
    {
        private SignalBus signalBus;
        private Cloud cloud;
        [Inject]
        private void Init(SignalBus signalBus, Cloud cloud)
        {
            this.signalBus = signalBus;
            this.cloud = cloud;
        }
        public SettingsData GetSettings
        {
            get => cloud.SettingsData;
        }

        public void Reset()
        {
            cloud.InputBindingsReset();
        }

        public bool OnPress(Action action)
        {
            return Input.GetKey(GetSettings.inputListenners[action]);
        }
        public void ChangeBinding(KeyCode oldKeyCode, KeyCode newKeyCode)
        {
            var targetAction = GetAction(oldKeyCode);
            if (GetSettings.inputListenners.ContainsValue(newKeyCode))
            {
                var affectedAction = GetAction(newKeyCode);
                if (affectedAction != Action.NONE)
                {
                    GetSettings.inputListenners[affectedAction] = oldKeyCode;
                }
            }

            GetSettings.inputListenners[targetAction] = newKeyCode;
        }

        private Action GetAction(KeyCode keyCode)
        {
            foreach (var item in GetSettings.inputListenners)
            {
                if (item.Value == keyCode) return item.Key;
            }

            return Action.NONE;
        }
    }
}