using System;
using System.Collections;
using AudioManager;
using Enums;
using InputManager;
using InputWrapper;
using So_Template;
using SpawnManager;
using StateManager.State.Template;
using UnityEngine;
using VideoManager;
using Zenject;
using Zenject.ProjectContext.Signals;

namespace StateManager
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
        protected Container.Cloud cloud;
        protected StateController<RunTimeState> runTimeStateController;
        protected InputController inputController;
        protected AudioController audioController;
        protected VideoController videoController;
        protected SceneManager.SceneManager sceneManager;
        protected SpawnController spawnController;
        protected MonoBehaviour monoBehaviour;
        protected GameStateSignal gameStateSignal;
        protected CompanyData companyData;
        private IEnumerator update;

        [Inject]
        private void Init(SignalBus signalBus,
            Container.Cloud cloud,
            StateController<RunTimeState> runTimeStateController,
            InputController inputController,
            AudioController audioController,
            VideoController videoController,
            MonoBehaviourSignal monoBehaviourSignal,
            SceneManager.SceneManager sceneManager,
            SpawnController spawnController,
            CompanyData companyData)
        {
            this.signalBus = signalBus;
            this.cloud = cloud;
            this.runTimeStateController = runTimeStateController;
            this.inputController = inputController;
            this.audioController = audioController;
            this.videoController = videoController;
            this.sceneManager = sceneManager;
            this.spawnController = spawnController;
            this.monoBehaviour = monoBehaviourSignal;
            this.companyData = companyData;
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
                currentState.Init(signalBus: signalBus,
                    cloud: cloud,
                    runTimeStateController: runTimeStateController,
                    inputController: inputController,
                    audioController: audioController,
                    videoController: videoController,
                    monoBehaviour: monoBehaviour,
                    sceneManager: sceneManager,
                    spawnController: spawnController,
                    companyData: companyData);
                
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