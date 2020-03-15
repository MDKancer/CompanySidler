using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using TMPro;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace StateMachine.States
{
    public class Loading : AState
    {
        private TextMeshProUGUI label;
        [Inject]
        protected override void Init(SignalBus signalBus,
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
            this.monoBehaviourSignal = monoBehaviourSignal;
            
        }

        public override void OnEnter()
        {
             label = GameObject.Find("Loading_Label").GetComponent<TextMeshProUGUI>();
        }

        public override void OnUpdate()
        {
            label.SetText($"Loading ... {sceneManager.SceneProgress}");
        }

        public override void OnExit()
        {
        }

        ~Loading()
        {
            
        }
    }
}