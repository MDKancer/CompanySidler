using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using Zenject;
using Zenject_Signals;

namespace StateMachine.States
{
    public abstract class AState : IState
    {
        protected SignalBus signalBus;
        protected Container container;
        protected StateController<GameState> gameStateController;
        protected StateController<RunTimeState> runTimeStateController;
        protected SceneManager sceneManager;
        protected SpawnController spawnController;
        protected MonoBehaviourSignal monoBehaviourSignal;
        protected GameStateSignal gameStateSignal;
        /// <summary>
        /// Here will be all global signals initialized, to make easily to handle.
        /// </summary>
        protected abstract void Init(SignalBus signalBus,
            Container container,
            StateController<GameState> gameStateController,
            StateController<RunTimeState> runTimeStateController,
            MonoBehaviourSignal monoBehaviourSignal,
            SceneManager sceneManager,
            SpawnController spawnController);

        /// <summary>
        /// Here the process is handled when the state signal is received.
        /// </summary>
        public abstract void OnEnter();

        /// <summary>
        /// After receiving the state signal,
        /// the processes are handled during the state update.
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        /// Before the change of the signal state,
        /// all processes that were necessary in this state are terminated
        /// or forwarded to the next state.
        /// </summary>
        public abstract void OnExit();
    }
}