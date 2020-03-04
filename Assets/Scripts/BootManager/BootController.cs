using System;
using System.Collections;
using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using StateMachine;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace BootManager
{
    public class BootController : MonoBehaviour
    {
        public static BootController BootControllerInstance { get; private set; }

        private SignalBus signalBus;
        private Container container;
        private StateController<GameState> gameStateController;
        private StateController<RunTimeState> runtimeStateController;
        private SpawnController spawnController;
        private SceneManager sceneManager;

        public MonoBehaviour monoBehaviour;
        
        public CompanyData companyData;
        public GameState gameState;

        [Inject]
        private void Init(SignalBus signalBus,Container container, StateController<GameState> gameStateController,
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
            switch (gameStateSignal.state)
            {
                case GameState.NONE:
                    break;
                case GameState.INTRO:
                    container.LoadAllResources();
                    break;
                case GameState.LOADING:
                    break;
                case GameState.MAIN_MENU:
                    break;
                case GameState.PREGAME:
                    break;
                case GameState.GAME:
                    runtimeStateController.CurrentState = RunTimeState.PLAYING;
                    container.SetDatas();
                    spawnController.InitialSpawnWave();
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
            if (BootControllerInstance == null)
            {
                BootControllerInstance = this;
                monoBehaviour = this;
                //___________________________________________________
                //gameStateController.CurrentState = gameState;
                //runtimeStateController.CurrentState = RunTimeState.NONE;
                
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            StartCoroutine(StartSimulating());
        }

        private IEnumerator StartSimulating()
        {
            int index = 0;
            while (gameStateController.CurrentState != gameState)
            {
                gameStateController.CurrentState =  (GameState)Enum.GetValues(typeof(GameState)).GetValue(index);
                //Debug.Log(gameStateController.CurrentState);
                index++;
                yield return new WaitForSeconds(0.3f);
            }
            
        }

    }
}