using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace StateMachine.States
{
    public class PreGame : AState
    {
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
            runTimeStateController.CurrentState = RunTimeState.NONE;
            Debug.Log($"Current State {this}");
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
        }

        ~PreGame()
        {
            
        }
    }
}