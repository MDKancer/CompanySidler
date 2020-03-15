using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace StateMachine.States
{
    public class Intro : AState
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
            Debug.Log($"Current State On Enter {this}");
            container.LoadAllResources();
            
            sceneManager.GoTo(Scenes.MAIN_MENU);
        }

        public override void OnUpdate()
        {
            Debug.Log($"Current State On Update {this}");
        }

        public override void OnExit()
        {
            Debug.Log($"Current State On Exit {this}");
        }

        ~Intro()
        {
            
        }
    }
}