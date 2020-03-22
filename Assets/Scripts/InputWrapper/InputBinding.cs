using System.Collections.Generic;
using Container;
using Enums;
using UnityEngine;

namespace InputWrapper
{
    public class InputBinding
    {
        private Cloud cloud;

        public Dictionary<Action,KeyCode> GetBindings
        {
            get => cloud.InputListenners;
        }

        public void Reset()
        {
            
        }

        public bool OnPress(Action action)
        {
            return Input.GetKey(GetBindings[action]);
        }
        public void ChangeBinding(KeyCode oldKeyCode, KeyCode newKeyCode)
        {
            var targetAction = GetAction(oldKeyCode);
            if (GetBindings.ContainsValue(newKeyCode))
            {
                var affectedAction = GetAction(newKeyCode);
                if (affectedAction != Action.NONE)
                {
                    GetBindings[affectedAction] = oldKeyCode;
                }
            }

            GetBindings[targetAction] = newKeyCode;
        }

        private Action GetAction(KeyCode keyCode)
        {
            foreach (var item in GetBindings)
            {
                if (item.Value == keyCode) return item.Key;
            }

            return Action.NONE;
        }
    }
}