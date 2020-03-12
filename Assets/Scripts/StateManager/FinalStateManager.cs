using System;
using System.ComponentModel;
using Enums;
using SceneController;
using SpawnManager;
using Zenject;
using Zenject_Signals;
using Container = GameCloud.Container;

namespace StateMachine
{
    public class FinalStateManager : IFSM
    {
        protected SignalBus signalBus;
        protected Container container;
        protected StateController<GameState> gameStateController;
        protected StateController<RunTimeState> runTimeStateController;
        protected SceneManager sceneManager;
        protected SpawnController spawnController;
        protected MonoBehaviourSignal monoBehaviourSignal;
        private GameStateSignal gameStateSignal;

        /// <summary>
        /// Here will be all global signals initialized, to make easily to handle
        /// </summary>
        [Inject]
        protected virtual void Init(
                SignalBus signalBus,
                Container container,
                StateController<GameState> gameStateController,
                StateController<RunTimeState> runTimeStateController,
                MonoBehaviourSignal monoBehaviourSignal,
                SceneManager sceneManager,
                SpawnController spawnController)
        {
            this.signalBus = signalBus;
            this.container = container;
            this.gameStateController = gameStateController;
            this.runTimeStateController = runTimeStateController;
            this.sceneManager = sceneManager;
            this.spawnController = spawnController;
            this.monoBehaviourSignal = monoBehaviourSignal;
            
            this.signalBus.Subscribe<GameStateSignal>(SignalHandel);
        }

        /// <summary>
        /// Handles the received signal state.
        /// </summary>
        private void SignalHandel(GameStateSignal stateSignal)
        {
            //When is the first state signal received
            if (this.gameStateSignal.Equals(null) && stateSignal.state != this.gameStateSignal.state)
            {
                this.gameStateSignal = stateSignal;
                OnEnter();
            }
            //When is the new state signal received
            else if (stateSignal.state != this.gameStateSignal.state)
            {
                OnExit();
                this.gameStateSignal = stateSignal;
                OnEnter();
            }
            //When  the state signal are changed
            else if (stateSignal.state == this.gameStateSignal.state)
            {
                OnUpdate();
            }
        }
        /// <summary>
        /// Here the process is handled when the state signal is received.
        /// </summary>
        public void OnEnter()
        {
            CheckStateOnEnter();
        }
        /// <summary>
        /// After receiving the state signal,
        /// the processes are handled during the state update.
        /// </summary>
        public void OnUpdate()
        {
            CheckStateOnUpdate();
        }
        /// <summary>
        /// Before the change of the signal state,
        /// all processes that were necessary in this state are terminated
        /// or forwarded to the next state.
        /// </summary>
        public void OnExit()
        {
            CheckStateOnExit();
        }
        
        /// <summary>
        /// The status is checked here to carry out the correct processes OnEnter event.
        /// <remarks>the process execution depends on the state.</remarks>
        /// </summary>
        protected virtual void CheckStateOnEnter()
        {
            switch (this.gameStateSignal.state)
            {
                case GameState.NONE:
                    throw new Exception($"GameState {this.gameStateSignal.state} is invalid");
                    break;
                case GameState.INTRO:
                    container.LoadAllResources();
                    break;
                case GameState.MAIN_MENU:
                    break;
                case GameState.LOADING:
                    break;
                case GameState.PREGAME:
                    runTimeStateController.CurrentState = RunTimeState.NONE;
                    break;
                case GameState.GAME:
                    container.SetDatas();
                    spawnController.InitialSpawnWave();
                    runTimeStateController.CurrentState = RunTimeState.PLAYING;
                    break;
                case GameState.EXIT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// The status is checked here to carry out the correct processes OnUpdate event.
        /// <remarks>the process execution depends on the state.</remarks>
        /// </summary>
        protected virtual void CheckStateOnUpdate()
        {
            switch (this.gameStateSignal.state)
            {
                case GameState.NONE:
                    throw new Exception($"GameState {this.gameStateSignal.state} is invalid");
                    break;
                case GameState.INTRO:
                    break;
                case GameState.MAIN_MENU:
                    break;
                case GameState.LOADING:
                    break;
                case GameState.PREGAME:
                    break;
                case GameState.GAME:
                    break;
                case GameState.EXIT:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// The status is checked here to carry out the correct processes OnExit event.
        /// <remarks>the process execution depends on the state.</remarks>
        /// </summary>
        protected virtual void CheckStateOnExit()
        {
            switch (this.gameStateSignal.state)
            {
                case GameState.NONE:
                    throw new Exception($"GameState {this.gameStateSignal.state} is invalid");
                    break;
                case GameState.INTRO:
                    break;
                case GameState.MAIN_MENU:
                    break;
                case GameState.LOADING:
                    break;
                case GameState.PREGAME:
                    break;
                case GameState.GAME:
                    break;
                case GameState.EXIT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        /// <summary>
        /// In deconstructor will be unsubscribed all zenject-signals what needed
        /// </summary>
        ~FinalStateManager()
        {
            this.signalBus.TryUnsubscribe<GameStateSignal>(OnEnter);
        }
    }
}