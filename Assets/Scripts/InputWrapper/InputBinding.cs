using System;
using System.Collections.Generic;
using Container;
using GameSettings;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Action = Enums.Action;

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

        public KeyboardsData[] KeyBoardData => cloud.InputKeyboardData;

        public bool OnPress(Action action)
        {
            return Input.GetKey(GetKeyCode(action));
        }
        public void ChangeBinding(KeyCode oldKeyCode, KeyCode newKeyCode)
        {
            var targetAction = GetAction(oldKeyCode);
            if (ContainsKeyCode(newKeyCode))
            {
                var affectedAction = GetAction(newKeyCode);
                if (affectedAction != Action.NONE)
                {
                    var keyboardsData = GetBiding(affectedAction);
                    keyboardsData.keyCode = oldKeyCode.ToString();
                }
            }

            var biding = GetBiding(targetAction);
            biding.keyCode = newKeyCode.ToString();
        }

        
        public void Reset()
        {
            cloud.SettingsDataReset();
        }
        private Action GetAction(KeyCode keyCode)
        {
            Action action = Action.NONE;
            foreach (var keyboards in KeyBoardData)
            {
                if (keyboards.keyCode.Equals(keyCode.ToString()))
                {
                    Enum.TryParse(keyboards.action, out action);
                    return action;
                }
            }
            return action;
        }
        private KeyCode GetKeyCode(Action action)
        {
            KeyCode keyCode = KeyCode.None;
            foreach (var keyboards in KeyBoardData)
            {
                if (keyboards.action.Equals(action.ToString()))
                {
                    Enum.TryParse(keyboards.keyCode, out keyCode);
                    return keyCode;
                }
            }

            return keyCode;
        }

        private bool ContainsKeyCode(KeyCode keyCode)
        {
            foreach (var keyboards in KeyBoardData)
            {
                if (keyboards.keyCode.Equals(keyCode.ToString())) return true;
            }

            return false;
        }

        private KeyboardsData GetBiding(Action action)
        {
            foreach (var keyboards in KeyBoardData)
            {
                if (keyboards.action.Equals(action.ToString()))
                {
                    return keyboards;
                }
            }

            return new KeyboardsData();
        }
    }
}