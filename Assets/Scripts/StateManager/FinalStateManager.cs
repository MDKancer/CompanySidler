using System.Collections;
using Container;
using Enums;
using StateManager.States.GameStates.Template;
using Zenject;
using Application = UnityEngine.Application;

namespace StateManager
{
    public class FinalStateManager : IFSM
    {
        protected SignalBus signalBus;
        protected Cloud cloud;
        protected StateMachineClass<AState> stateMachineClass;

        /// <summary>
        /// Here will be all global signals initialized, to make easily to handle
        /// </summary>
        [Inject]
        protected virtual void Init(
                SignalBus signalBus,
                Cloud cloud,
                StateMachineClass<AState> stateMachineClass)
        {
            this.signalBus = signalBus;
            this.stateMachineClass = stateMachineClass;
            this.cloud = cloud;
            //first need to exit from the last State
            this.stateMachineClass.onStateExit += OnExit;
            //than initialize the currentState
            this.stateMachineClass.onStateEnter += OnEnter;
            //will be On Update inside current state in loop executed
            this.stateMachineClass.onStateUpdated += OnUpdate;
            Application.quitting += OnQuitting;
        }

        private void OnQuitting()
        {
            stateMachineClass.CurrentState = cloud.GetGameState(Scenes.EXIT);
        }
        /// <summary>
        /// Here the process is handled when the state signal is received.
        /// </summary>
        public void OnEnter()
        {
                stateMachineClass.CurrentState.OnStateEnter();
        }
        /// <summary>
        /// After receiving the state signal,
        /// the processes are handled during the state update.
        /// </summary>
        public IEnumerator OnUpdate()
        {
            while (true)
            {
                stateMachineClass.CurrentState.OnStateUpdate();
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
            stateMachineClass.CurrentState.OnStateExit();
        }

        /// <summary>
        /// In deconstructor will be unsubscribed all zenject-signals what needed
        /// </summary>
        ~FinalStateManager()
        {
            this.stateMachineClass.onStateEnter -= OnExit;
            this.stateMachineClass.onStateEnter -= OnEnter;
            this.stateMachineClass.onStateUpdated -= OnUpdate;
            Application.quitting -= OnQuitting;
        }
    }
}
