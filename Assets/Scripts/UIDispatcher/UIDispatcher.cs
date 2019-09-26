using System;
using BootManager;
using BuildingPackage;
using Constants;
using Credits;
using Life;
using InputManager;
using NaughtyAttributes;
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
        [Required]
        public GameObject playerView;
        private CreditsManager creditsManager;
        private GameObject creditPanel;
        private int workerCount = 0;

        private void Awake()
        {
                if (uiDispatcher == null)
                {
                        uiDispatcher = this;
                        creditsManager = GetComponent<CreditsManager>();
                        playerView = GameObject.Find("BuildingInfo");
                        playerView.SetActive(false);
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
                ShowBuildingInfoWindow();
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
        public void ApplyWorker(String name)
        {
                Vector3  spawnPosition = new Vector3(4f,1f,2f);
                //Boot.spawnController.SpawnObject(Boot.container.GetPrefabsByType(EntityType.DEVELOPER)[0], spawnPosition,EntityType.DEVELOPER);
                HumanData humanData = new HumanData(GetValue(name));
                humanData.GetHisOffice = Building.BuildingData.buildingType;
                
                Boot.spawnController.SpawnObject(humanData,spawnPosition);
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

        public void UpgradeBuilding()
        {
                if (Boot.runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO ) // && Boot.gameStateController.CurrentState == GameState.GAME
                {
                        Building.Upgrade();
                }
        }

        public void ChangeWorkingBuildingState(Transform button)
        {
                if (Boot.runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO ) // && Boot.gameStateController.CurrentState == GameState.GAME
                {
                        Building.SwitchWorkingState();
                        
                        TextMeshProUGUI btnText = button.GetComponent<TextMeshProUGUI>();
                        btnText.SetText(
                                btnText.text.ToLower().Contains(BuildingState.PAUSE.ToString().ToLower()) ? 
                                        BuildingState.WORK.ToString() : BuildingState.PAUSE.ToString()
                                        );
                } 
        }

        private void ShowBuildingInfoWindow()
        {
                if (Boot.runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO) // && Boot.gameStateController.CurrentState == GameState.GAME
                {
                        playerView.SetActive(true);
                        
                        GameObject.Find("BuildingTitle").GetComponent<TextMeshProUGUI>().SetText(Building.BuildingData.name);
                        GameObject.Find("WorkerCount").GetComponent<TextMeshProUGUI>().SetText(Building.BuildingData.workers.ToString());
                        GameObject.Find("WorkerLimit").GetComponent<TextMeshProUGUI>().SetText("/ "+Building.BuildingData.workPlacesLimit.ToString());
                        GameObject.Find("Price").GetComponent<TextMeshProUGUI>().SetText(Building.BuildingData.upgradePrice.ToString());
                        GameObject.Find("Geld").GetComponent<TextMeshProUGUI>().SetText(Building.BuildingData.moneyPerSec.ToString());
                        
                        float currentSize = (Building.BuildingData.currenHhitPoints * 100f /
                                            Building.BuildingData.maxHitPoints) / 100f;
                        GameObject.Find("HitPoints").transform.localScale = new Vector3( currentSize, 1f,1f);
                }
                else if(Boot.runtimeStateController.CurrentState != RunTimeState.BUILDING_INFO && Boot.runtimeStateController.CurrentState != RunTimeState.GAME_MENU)
                {
                        
                        playerView.SetActive(false);
                }
        }

        private iBuilding Building
        {
                get => InputController.FocusedBuilding?.GetComponent(typeof(iBuilding)) as iBuilding;
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

        private EntityType GetValue(String value)
        {
                return (EntityType) Enum.Parse(typeof(EntityType), value.ToUpper());
        }
}
