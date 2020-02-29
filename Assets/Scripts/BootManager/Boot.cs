using System;
using Enums;
using GameCloud;
using SceneController;
using Signals.Zenject_Test;
using SpawnManager;
using StateMachine;
using UnityEngine;
using Zenject;

namespace BootManager
{
    public class Boot : MonoBehaviour
    {
        public static Boot boot_Instance { get; private set; }

        //public ReferenceService referenceService;
        [Inject] public SignalBus signalBus;
        [Inject]
        public Container container;
        [Inject]
        public StateController<GameState> gameStateController;
        [Inject]
        public StateController<RunTimeState> runtimeStateController;
        [Inject]
        public SpawnController spawnController;
        [Inject]
        public SceneManager sceneManager;

        public MonoBehaviour monoBehaviour;
        
        public CompanyData companyData;
        public GameState gameState;

        /// <summary>
        /// Hier wird alle Hauptklassen intizialisiert.
        /// Awake wird in jede Scene erst ausgeführt. 
        /// </summary>
        //void Awake(Container container, StateController<GameState> gameStateController,StateController<RunTimeState> runtimeStateController,SpawnController spawnController,SceneManager sceneManager)
        void Awake()
        {
            
            if (boot_Instance == null)
            {
                //___________________________________________________
                //Es wird alle wichtige Klasse was nur ein mal für
                //das ganzes Spiel instanzieren sollen.
                //___________________________________________________
                boot_Instance = this;
                monoBehaviour = this;
                
                //____________________________________________________
                // 
                //___________________________________________________
                gameStateController.CurrentState = gameState;
                runtimeStateController.CurrentState = RunTimeState.NONE;
                // wenn mann von Main Menu anfangen moechte.... dann kommentieren.

                container.LoadAllResources();
                
                
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
                
            GameStarted();
        }

        private void Start()
        {
            signalBus.Fire(new CustomerSignals
            {
                name = "bulit"
            });
        }

        /// <summary>
        /// Hier wird die Daten fürs anfangs fest gesetzt wie man an anfangs des GamePlay braucht.
        /// </summary>
        private void SetGamePlayData()
        {
            container.SetDatas();
        }
    
        private void GameStarted()
        {
            if (gameStateController.CurrentState == GameState.GAME)
            {
                runtimeStateController.CurrentState = RunTimeState.PLAYING;
                SetGamePlayData();
                // Es ist noch unter die Frage.......
                spawnController.InitialSpawnWave();
            }
        }
    }
}