using System.Collections;
using StateMachine.States;
using UnityEngine;
using Zenject;
using Zenject_Signals;
using Container = GameCloud.Container;

namespace StateMachine
{
    public class FinalStateManager_Test : IFSM_Test
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
            
            //first need to exit from the last State
            this.stateMachineClass.stateChanged += OnExit;
            //than initialize the currentState
            this.stateMachineClass.stateChanged += OnEnter;
            this.stateMachineClass.currentStateUpdated += OnUpdate;
            
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
        public IEnumerator OnUpdate()
        {
            while (true)
            {
                stateMachineClass.CurrentState.OnUpdate();
                yield return null;
            }
        }
        /// <summary>
        /// Before the change of the signal state,
        /// all processes that were necessary in this state are terminated
        /// or forwarded to the next state.
        /// </summary>
        public void OnExit()
        {
            stateMachineClass.CurrentState.OnExit();
            // aici trebuie sa execut mai intaii vechiul Status 
            // si dupa curentul
            // sau doar currentul?
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