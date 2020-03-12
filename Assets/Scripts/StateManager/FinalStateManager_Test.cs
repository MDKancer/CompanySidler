using StateMachine.States;
using Zenject;
using Zenject_Signals;
using Container = GameCloud.Container;

namespace StateMachine
{
    public class FinalStateManager_Test : IFSM
    {
        protected SignalBus signalBus;
        protected Container container;
        protected StateMachineClass<AState> stateMachineClass;
        protected MonoBehaviourSignal monoBehaviourSignal;
        private GameStateSignal gameStateSignal;

        /// <summary>
        /// Here will be all global signals initialized, to make easily to handle
        /// </summary>
        [Inject]
        protected virtual void Init(
                SignalBus signalBus,
                StateMachineClass<AState> stateMachineClass,
                MonoBehaviourSignal monoBehaviourSignal)
        {
            this.signalBus = signalBus;
            this.stateMachineClass = stateMachineClass;
            this.monoBehaviourSignal = monoBehaviourSignal;
            
            this.signalBus.Subscribe<GameStateSignal>(SignalHandel);
        }

        /// <summary>
        /// Handles the received signal state.
        /// </summary>
        private void SignalHandel(GameStateSignal stateSignal)
        {
            this.gameStateSignal = stateSignal;
        }
        /// <summary>
        /// Here the process is handled when the state signal is received.
        /// </summary>
        public void OnEnter()
        {
                stateMachineClass.CurrentState.OnEnter();
        }
        /// <summary>
        /// After receiving the state signal,
        /// the processes are handled during the state update.
        /// </summary>
        public void OnUpdate()
        {
            stateMachineClass.CurrentState.OnUpdate();
        }
        /// <summary>
        /// Before the change of the signal state,
        /// all processes that were necessary in this state are terminated
        /// or forwarded to the next state.
        /// </summary>
        public void OnExit()
        {
            stateMachineClass.CurrentState.OnExit();
        }

        /// <summary>
        /// In deconstructor will be unsubscribed all zenject-signals what needed
        /// </summary>
        ~FinalStateManager_Test()
        {
            this.signalBus.TryUnsubscribe<GameStateSignal>(OnEnter);
        }
    }
}