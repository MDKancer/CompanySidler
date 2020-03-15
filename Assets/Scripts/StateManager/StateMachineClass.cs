using System;
using System.Collections;
using System.Diagnostics.Tracing;
using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using Zenject;
using Zenject_Signals;

namespace StateMachine.States
{
    /// <summary>
    /// It is a based class state machine.
    /// <remarks>Are accepted only the classes that inherit from AState or IState</remarks>
    /// </summary>
    public class StateMachineClass<T> where T : AState,IState
    {
        internal delegate void OnStateEnter();

        internal delegate IEnumerator OnStateUpdated();
        /// <summary>
        /// When a new current state was inserted.
        /// </summary>
        internal event OnStateEnter stateChanged;
        /// <summary>
        /// When the current state is updated
        /// </summary>
        internal event OnStateUpdated currentStateUpdated;
        
        private T currentState;
        private T lastState;
        protected SignalBus signalBus;
        protected Container container;
        protected StateController<RunTimeState> runTimeStateController;
        protected SceneManager sceneManager;
        protected SpawnController spawnController;
        protected MonoBehaviour monoBehaviour;
        protected GameStateSignal gameStateSignal;
        private IEnumerator update;

        [Inject]
        private void Init(SignalBus signalBus,
            Container container,
            StateController<RunTimeState> runTimeStateController,
            MonoBehaviourSignal monoBehaviourSignal,
            SceneManager sceneManager,
            SpawnController spawnController)
        {
            this.signalBus = signalBus;
            this.container = container;
            this.runTimeStateController = runTimeStateController;
            this.sceneManager = sceneManager;
            this.spawnController = spawnController;
            this.monoBehaviour = monoBehaviourSignal;
        }
        public T CurrentState
        {
            set
            { 
                lastState = currentState != null ? currentState : lastState;
                
                if(currentState != null)
                {
                    //before to end the last state, should be to stop the update for the last state
                    //OnUpdate()
                    monoBehaviour.StopCoroutine(update);
                    
                    //before the current state is changed, should be to exit from the last state
                    //OnExit()
                    stateChanged.GetInvocationList()[0].DynamicInvoke();
                }
                currentState = value;
                
                // the important fields send on to the current state
                currentState.Init(signalBus,container,runTimeStateController,monoBehaviour,sceneManager,spawnController);
                
                //now the current state was changed, and that mean the state is initialized
                // OnEnter()
                stateChanged.GetInvocationList()[1].DynamicInvoke();
                
                //set the coroutine to start the OnUpdate()
                update = currentStateUpdated.Invoke();
                //started a coroutine for the update
                //OnUpdate()
                monoBehaviour.StartCoroutine(update);
            }
            get => currentState;
        }
        public T LastState
        {
            get => lastState;
            private set => lastState = value;
        }

        public void SwitchToLastState()
        {
            try
            {
                var temp = LastState;
                LastState = CurrentState;
                CurrentState = temp;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                Console.WriteLine(e);
            }
        }
    }
}