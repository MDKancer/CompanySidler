using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace StateMachine.States
{
    public class Exit : AState
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
            Application.Quit();
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
            
        }

        ~Exit()
        {
            
        }
    }
}