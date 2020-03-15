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

        public override void Init(SignalBus signalBus,
            Container container,
            StateController<RunTimeState> runTimeStateController,
            MonoBehaviour monoBehaviour,
            SceneManager sceneManager,
            SpawnController spawnController)
        {
            this.signalBus = signalBus;
            this.container = container;
            this.runTimeStateController = runTimeStateController;
            this.sceneManager = sceneManager;
            this.spawnController = spawnController;
            this.monoBehaviour = monoBehaviour;
            
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