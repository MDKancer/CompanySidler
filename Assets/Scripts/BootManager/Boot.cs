using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using StateMachine;
using UnityEngine;

namespace BootManager
{
    public class Boot : MonoBehaviour
    {
        public static Boot boot_Instance { get; private set; }

        public static MonoBehaviour monobehaviour;
        public static Container container;
        public static StateController<GameState> gameStateController;
        public static StateController<RunTimeState> runtimeStateController;
        public static SpawnController spawnController;
        public static SceneManager sceneManager;
        public CompanyData companyData;
        public GameState gameState;

        /// <summary>
        /// Hier wird alle Hauptklassen intizialisiert.
        /// Awake wird in jede Scene erst ausgeführt. 
        /// </summary>
        void Awake()
        {
            
            if (boot_Instance == null)
            {
                //___________________________________________________
                //Es wird alle wichtige Klasse was nur ein mal für
                //das ganzes Spiel instanzieren sollen.
                //___________________________________________________
                boot_Instance = this;
                monobehaviour = this;
                container = new Container();
                gameStateController = new StateController<GameState>();
                runtimeStateController = new StateController<RunTimeState>();
                spawnController = new SpawnController();
                sceneManager = new SceneManager();
                
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