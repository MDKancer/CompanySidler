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
        // Inainte de a schimba scena dorita, eu chem scena loading pentru a incarca scena dorita.
        public override void OnEnter()
        {
            container.LoadAllResources();
            
            //TODO : Löschen
            this.signalBus.Fire(new GameStateSignal
            {
                state =  GameState.INTRO
            });
            //TODO : Hier muss ich irgendwie  den Loading State zwieschen State einbauen
            
            sceneManager.GoTo(Scenes.MAIN_MENU);
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

        ~Intro()
        {
            
        }
    }
}