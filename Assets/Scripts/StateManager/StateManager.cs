using System;
using UnityEngine;

namespace StateMachine
{
    public class StateController<T>
    {
        private T currentState;
        private T lastState;
        
        public T CurrentState
        {
            set
            {
               lastState = currentState != null ? currentState : lastState;
                
                currentState = value;
            }
            get { return currentState; }
        }
        public T LastState
        {
            private set { currentState = value; }
            get { return currentState; }
        }

        public void SwitchToLastState()
        {
            T temp;
            
            try
            {
                temp = lastState;
                LastState = currentState;
                currentState = temp;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
    }
}