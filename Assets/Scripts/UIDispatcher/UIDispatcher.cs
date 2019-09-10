using BootManager;
using Constants;
using Credits;
using TMPro;
using UnityEngine;
/// <summary>
/// UIDispatcher is a Singleton.
/// </summary>
public class UIDispatcher : MonoBehaviour
{
        public static UIDispatcher uiDispatcher;
        // Es ist nur temporär um zu wissen ob alles gut läuft.
        public  GameState currentGameState;
        public static int currentBuget;
        
        private CreditsManager creditsManager;
        private GameObject creditPanel;
        private int workerCount = 0;

        private void Awake()
        {
                if (uiDispatcher == null)
                {
                        uiDispatcher = this;
                        creditsManager = GetComponent<CreditsManager>();
                        DontDestroyOnLoad(gameObject);
                }
                else
                {
                        Destroy(gameObject);
                }
                
                creditPanel = GameObject.Find("CreditsPanel");
                creditPanel?.SetActive(false);
        }

        private void Update()
        {
                CheckCurrentState();
                CurrentBuget();
        }

        public void PlayGame()
        {
                //first
                Boot.gameStateController.CurrentState = GameState.GAME;
                //second
                Boot.sceneManager.GoTo(Scenes.GAME);
        }

        public void Options()
        {
        }

        public void Credits()
        {
                creditPanel.SetActive(true);
        }

        public void Exit()
        {
                //first
                Boot.gameStateController.CurrentState = GameState.EXIT;
                //second
                Application.Quit();
        }

        public void ShowWorkerList(Transform target)
        {
                target.gameObject.SetActive(!target.gameObject.active);
                if(Boot.runtimeStateController.CurrentState != RunTimeState.GAME_MENU)
                {
                        Boot.runtimeStateController.CurrentState = RunTimeState.GAME_MENU;
                }
                else
                {
                        Boot.runtimeStateController.SwitchToLastState();
                }
        }
        public void ApplyWorker()
        {
                Vector3  spawnPosition = new Vector3(4f,1f,2f);
                Boot.spawnController.SpawnObject(Boot.container.GetPrefabsByType(EntityType.WORKER)[0], spawnPosition);
                workerCount++;
                TextMeshProUGUI workersCount = GameObject.Find("Panel_Mitarbeiter_Nr").GetComponent<TextMeshProUGUI>();
                workersCount.SetText(workerCount.ToString());
        }

        public void ApplyProject()
        {
                
        }

        public void BuyBuilding()
        {
                
        }
        
        /// <summary>
        /// Es ist nur temporär um zu wissen ob alles gut Läuft.
        /// </summary>
        private void CheckCurrentState()
        {
                if(Boot.gameStateController != null)
                {
                        if (currentGameState != Boot.gameStateController.CurrentState)
                        {
                                currentGameState = Boot.gameStateController.CurrentState;
                        }
                }
        }

        private void CurrentBuget()
        {
               // if(Boot.gameStateController.CurrentState == GameState.GAME)
                //{
                        TextMeshProUGUI money = GameObject.Find("Panel_Geld_Nr")?.GetComponent<TextMeshProUGUI>();
                        money?.SetText(currentBuget.ToString());
                //}
        }
}
