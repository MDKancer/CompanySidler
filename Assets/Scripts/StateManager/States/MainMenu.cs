using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace StateMachine.States
{
    public class MainMenu : AState
    {
        [Inject]
        protected override void Init(SignalBus signalBus, Container container, StateController<GameState> gameStateController,
            StateController<RunTimeState> runTimeStateController, MonoBehaviourSignal monoBehaviourSignal, SceneManager sceneManager,
            SpawnController spawnController)
        {
            this.signalBus = signalBus;
            this.container = container;
            this.gameStateController = gameStateController;
            this.runTimeStateController = runTimeStateController;
            this.sceneManager = sceneManager;
            this.spawnController = spawnController;
            this.monoBehaviourSignal = monoBehaviourSignal;
            
        }

        public override void OnEnter()
        {
            Debug.Log($"Current State On Enter {this}");
        }

        public override void OnUpdate()
        {
            Debug.Log($"Current State On Update {this}");
        }

        public override void OnExit()
        {
            Debug.Log($"Current State On Exit {this}");
        }

        ~MainMenu()
        {
            
        }
    }
}