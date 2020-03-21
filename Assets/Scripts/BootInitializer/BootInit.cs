using System;
using StateManager;
using StateManager.State;
using StateManager.State.Template;
using UnityEngine;
using Zenject;

namespace BootInitializer
{
    public class BootInit : MonoBehaviour
    {
        private StateMachineClass<AState> stateMachineClass;
    
        [Inject]
        private void Init(StateMachineClass<AState> stateMachineClass)
        {
            this.stateMachineClass = stateMachineClass;
        }
    
        // Start is called before the first frame update
        void Start()
        {
            try
            {
                // initialize the game
                stateMachineClass.CurrentState = new Intro();
            }
            catch (Exception e)
            {
                Debug.Log($" The {this}.Init method wasn't executed");
                Debug.LogError(e);
                throw;
            }
        }
    }
}
