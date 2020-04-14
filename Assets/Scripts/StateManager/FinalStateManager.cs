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
            this.stateMachineClass.stateChanged += OnExit;
            //than initialize the currentState
            this.stateMachineClass.stateChanged += OnEnter;
            //will be On Update inside current state in loop executed
            this.stateMachineClass.currentStateUpdated += OnUpdate;
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
        }

        /// <summary>
        /// In deconstructor will be unsubscribed all zenject-signals what needed
        /// </summary>
        ~FinalStateManager()
        {
            this.stateMachineClass.stateChanged -= OnExit;
            this.stateMachineClass.stateChanged -= OnEnter;
            this.stateMachineClass.currentStateUpdated -= OnUpdate;
            Application.quitting -= OnQuitting;
        }
    }
}
