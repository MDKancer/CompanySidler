using System;
using System.Collections;
using Enums;
using GameCloud;
using SceneController;
using Sirenix.OdinInspector;
using SpawnManager;
using StateMachine;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace BootManager
{
    public class BootController
    {
        [ToggleGroup("simulatorState")]
        public bool simulatorState = false;
        private SignalBus signalBus;
        private Container container;
        private StateController<GameState> gameStateController;
        private StateController<RunTimeState> runtimeStateController;
        private SpawnController spawnController;
        private SceneManager sceneManager;
        
        [Title("Game State"),EnumToggleButtons, HideLabel]
//        public GameState gameState;

        [Inject]
        public void Init(SignalBus signalBus,Container container, StateController<GameState> gameStateController,
            StateController<RunTimeState> runtimeStateController, SpawnController spawnController,
            SceneManager sceneManager)
        {
            
                this.signalBus = signalBus;
                this.container = container;
                this.gameStateController = gameStateController;
                this.runtimeStateController = runtimeStateController;
                this.spawnController = spawnController;
                this.sceneManager = sceneManager;
                this.signalBus.Subscribe<GameStateSignal>(StateDependency);
            
        }

        private void StateDependency(GameStateSignal gameStateSignal)
        {
            //Debug.Log($"gameStateController{gameStateController} currentScene {gameStateSignal.state}");
            switch (gameStateSignal.state)
            {
                case GameState.NONE:
                    break;
                case GameState.INTRO:
                    break;
                case GameState.LOADING:
                    break;
                case GameState.MAIN_MENU:
                    container.LoadAllResources();
                    break;
                case GameState.PREGAME:
                    runtimeStateController.CurrentState = RunTimeState.NONE;
                    break;
                case GameState.GAME:
                    container.SetDatas();
                    spawnController.InitialSpawnWave();
                    runtimeStateController.CurrentState = RunTimeState.PLAYING;
                    break;
                case GameState.EXIT:
                    break;
            }
        }
        /// <summary>
        /// Hier wird alle Hauptklassen intizialisiert.
        /// Awake wird in jede Scene erst ausgeführt. 
        /// </summary>
        void Awake()
        {
//            if (BootControllerInstance == null)
//            {
//                BootControllerInstance = this;
//                //___________________________________________________
//                
//                //gameStateController.CurrentState = gameState;
//              
//                
//                DontDestroyOnLoad(gameObject);
//            }
//            else
//            {
//                Destroy(gameObject);
//            }
        }

        private void Start()
        {
            if(simulatorState)
            {
//                monoBehaviour.StartCoroutine(StartSimulating());
            }
        }


//        private IEnumerator StartSimulating()
//        {
//            int index = 0;
////            while (gameStateController.CurrentState != gameState)
////            {
////                gameStateController.CurrentState =  (GameState)Enum.GetValues(typeof(GameState)).GetValue(index);
////                //Debug.Log(gameStateController.CurrentState);
////                index++;
////                yield return new WaitForSeconds(0.3f);
////            }
//            
//        }
    }
}