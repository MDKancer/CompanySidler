using System.Collections.Generic;
using Container;
using Enums;
using UnityEngine;

namespace InputWrapper
{
    public class InputBinding
    {
        private Cloud cloud;

        public Dictionary<Actions,KeyCode> GetBindings
        {
            get => cloud.InputListenners;
        }

        public void ChangeBinding(KeyCode oldkeyCode, KeyCode newkeyCode)
        {
            var targetAction = GetAction(oldkeyCode);
            if (GetBindings.ContainsValue(newkeyCode))
            {
                var affectedAction = GetAction(newkeyCode);
                if (affectedAction != Actions.NONE)
                {
                    GetBindings[affectedAction] = oldkeyCode;
                }
            }

            GetBindings[targetAction] = newkeyCode;
        }

        private Actions GetAction(KeyCode keyCode)
        {
            foreach (var item in GetBindings)
            {
                if (item.Value == keyCode) return item.Key;
            }

            return Actions.NONE;
        }
    }
}