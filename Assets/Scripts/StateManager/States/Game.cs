using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using Zenject;
using Zenject_Signals;

namespace StateMachine.States
{
    public class Game : AState
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
            container.SetDatas();
            spawnController.InitialSpawnWave();
            runTimeStateController.CurrentState = RunTimeState.PLAYING;
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
        }

        ~Game()
        {
            
        }
    }
}