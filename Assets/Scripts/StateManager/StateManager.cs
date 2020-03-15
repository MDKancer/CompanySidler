using System;
using Enums;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace StateMachine
{
    /// <summary>
    /// It is a enum class state machine.
    /// </summary>
    public class StateController<T> where T : Enum
    {
        private T currentState;
        private T lastState;
        private SignalBus signalBus;

        [Inject]
        private void Init(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }
        public T CurrentState
        {
            set
            { 
               lastState = currentState != null ? currentState : lastState;
                
                currentState = value;
            }
            get => currentState;
        }
        public T LastState
        {
            private set => currentState = value;
            get => currentState;
        }

        public void SwitchToLastState()
        {
            try
            {
                var temp = lastState;
                LastState = currentState;
                currentState = temp;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
}