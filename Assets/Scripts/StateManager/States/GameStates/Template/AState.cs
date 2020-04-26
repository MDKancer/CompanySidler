using AudioManager;
using Container;
using Enums;
using InputManager;
using So_Template;
using SpawnManager;
using StateManager.States.EmploeeStates;
using UnityEngine;
using UnityEngine.Events;
using VideoManager;
using Zenject;
using Zenject.ProjectContext.Signals;

namespace StateManager.States.GameStates.Template
{
    public abstract class AState : IState
    {
        public delegate void OnCompleted();
        public OnCompleted onCompleted;
        protected SignalBus signalBus;
        protected Cloud cloud;
        protected StateController<RunTimeState> runTimeStateController;
        protected InputController inputController;
        protected AudioController audioController;
        protected VideoController videoController;
        protected SceneManager.SceneManager sceneManager;
        protected SpawnController spawnController;
        protected MonoBehaviour monoBehaviour;
        protected GameStateSignal gameStateSignal;
        protected CompanyData companyData;

        /// <summary>
        /// Here will be all global signals initialized, to make easily to handle.
        /// </summary>
        public abstract void Init(SignalBus signalBus,
            Cloud cloud,
            StateController<RunTimeState> runTimeStateController,
            InputController inputController,
            AudioController audioController,
            VideoController videoController,
            MonoBehaviour monoBehaviour,
            SceneManager.SceneManager sceneManager,
            SpawnController spawnController,
            CompanyData companyData);

        /// <summary>
        /// Here the process is handled when the state signal is received.
        /// </summary>
        public abstract void OnStateEnter();

        /// <summary>
        /// After receiving the state signal,
        /// the processes are handled during the state update.
        /// </summary>
        public abstract void OnStateUpdate();

        /// <summary>
        /// Before the change of the signal state,
        /// all processes that were necessary in this state are terminated
        /// or forwarded to the next state.
        /// </summary>
        public abstract void OnStateExit();
    }
}