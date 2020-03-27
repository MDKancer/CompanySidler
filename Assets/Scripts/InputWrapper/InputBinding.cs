using Container;
using Enums;
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

        public void Reset()
        {
            cloud.SettingsDataReset();
        }

        public bool OnPress(Action action)
        {
            return Input.GetKey(cloud.InputKeyboardData[action]);
        }
        public void ChangeBinding(KeyCode oldKeyCode, KeyCode newKeyCode)
        {
            var targetAction = GetAction(oldKeyCode);
            if (cloud.InputKeyboardData.ContainsValue(newKeyCode))
            {
                var affectedAction = GetAction(newKeyCode);
                if (affectedAction != Action.NONE)
                {
                    cloud.InputKeyboardData[affectedAction] = oldKeyCode;
                }
            }

            cloud.InputKeyboardData[targetAction] = newKeyCode;
        }

        private Action GetAction(KeyCode keyCode)
        {
            foreach (var item in cloud.InputKeyboardData)
            {
                if (item.Value == keyCode) return item.Key;
            }
            return Action.NONE;
        }
    }
}