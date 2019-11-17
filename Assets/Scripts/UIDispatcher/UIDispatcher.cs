using System;
using System.Globalization;
using BootManager;
using BuildingPackage;
using Enums;
using Credits;
using Human;
using InputManager;
using NaughtyAttributes;
using TMPro;
using UIPackage.UIBuildingContent;
using UnityEngine;

namespace UIPackage
{
        /// <summary>
        /// UIDispatcher is a Singleton.
        /// </summary>
        public class UIDispatcher : MonoBehaviour
        {
                public static UIDispatcher uiDispatcher;
                public  GameState currentGameState;
                //public static int currentBudget;
                [Required]
                public GameObject buildingInfo;

                private GameObject buildingContentObj;

                private UIData uiData = new UIData();
                private CreditsManager creditsManager;
                private GameObject creditPanel;
                private int workerCount = 0;
                private bool haveContentBtn = false;
                private BuildingContent buildingContent;

                private TextMeshProUGUI budget_Label;
                private TextMeshProUGUI numberOfCustomers_Label;
                private TextMeshProUGUI workersCount_Label;
                private TextMeshProUGUI buildingTitle_Label;
                private TextMeshProUGUI employeeCount_Label;
                private TextMeshProUGUI employeeLimit_Label;
                private TextMeshProUGUI price_Label;
                private TextMeshProUGUI currentBudget_Label;
                private GameObject curentHitPoints;

                private void Awake()
                {
                        if (uiDispatcher == null)
                        {
                                uiDispatcher = this;
                                creditsManager = GetComponent<CreditsManager>();
                                buildingContentObj = GameObject.Find("BuildingContent");
                                buildingInfo = GameObject.Find("BuildingInfo");
                                budget_Label = GameObject.Find("Panel_Geld_Nr")?.GetComponent<TextMeshProUGUI>();
                                numberOfCustomers_Label = GameObject.Find("Panel_Kunde_Nr")?.GetComponent<TextMeshProUGUI>();
                                workersCount_Label = GameObject.Find("Panel_Mitarbeiter_Nr").GetComponent<TextMeshProUGUI>();
                                
                                buildingTitle_Label = GameObject.Find("BuildingTitle").GetComponent<TextMeshProUGUI>();
                                employeeCount_Label = GameObject.Find("WorkerCount").GetComponent<TextMeshProUGUI>();
                                employeeLimit_Label = GameObject.Find("WorkerLimit").GetComponent<TextMeshProUGUI>();
                                price_Label = GameObject.Find("Price").GetComponent<TextMeshProUGUI>();
                                currentBudget_Label = GameObject.Find("Geld").GetComponent<TextMeshProUGUI>();
                                curentHitPoints = GameObject.Find("HitPoints");
                                
                                buildingInfo.SetActive(false);
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
                        CurrentBudget();
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

            
                public void ApplyWorker(String name)
                {
                        var  spawnPosition = new Vector3(4f,1f,2f);
                        var humanData = new EmployeeData(GetValue(name),Building.BuildingData.buildingType);

                        Boot.spawnController.SpawnWorker(humanData,spawnPosition);
                        workerCount++;
                        workersCount_Label.SetText(workerCount.ToString());
                }

                public void ApplyProject()
                {
                        if (Boot.runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO ) // && Boot.gameStateController.CurrentState == GameState.GAME
                        {
                              //Building.ApplyProject();
                        }
                }

                public void BuyBuilding()
                {
                        
                }

                public void UpgradeBuilding()
                {
                        if (Boot.runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO ) // && Boot.gameStateController.CurrentState == GameState.GAME
                        {
                                if(Building.Company.CurrentBudget >= Building.BuildingData.upgradePrice)
                                {
                                        Building.Upgrade();
                                        // content refresh
                                        RemoveBuildingContent();
                                }
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
                                buildingInfo.SetActive(true);
                                SetBuildingDataInContent();
                               
                                
                                float currentSize = (Building.BuildingData.currentHitPoints * 100f /
                                                    Building.BuildingData.maxHitPoints) / 100f;
                                curentHitPoints.transform.localScale = new Vector3( currentSize, 1f,1f);
                                if (!haveContentBtn)
                                {
                                        GenerateBuildingContent();
                                }
                        }
                        else if(Boot.runtimeStateController.CurrentState != RunTimeState.BUILDING_INFO && Boot.runtimeStateController.CurrentState != RunTimeState.GAME_MENU)
                        {
                                RemoveBuildingContent();
                                buildingInfo.SetActive(false);
                        }
                }
                private Building Building => (Building) InputController.FocusedBuilding?.GetComponent(typeof(Building));

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

                private void GenerateBuildingContent()
                {
                        buildingContent = new BuildingContent(Building, buildingContentObj);
                        buildingContent.CreateBuildingContent(ref uiData);

                        haveContentBtn = true;
                }
                private void RemoveBuildingContent()
                {
                        uiData.RemoveAll();

                        haveContentBtn = false;
                }
                private void CurrentBudget()
                {
                       // if(Boot.gameStateController.CurrentState == GameState.GAME)
                        //{
                                
                                budget_Label?.SetText(Boot.container.Companies[0].CurrentBudget.ToString());
                        //}
                }

                private EntityType GetValue(String value) => (EntityType) Enum.Parse(typeof(EntityType), value.ToUpper());
                

                private void SetBuildingDataInContent()
                {
                        
                        buildingTitle_Label.SetText(Building.BuildingData.name);
                        
                        employeeCount_Label.SetText(Building.BuildingData.workers.ToString());
                        
                        employeeLimit_Label.SetText("/ "+Building.BuildingData.workPlacesLimit);
                        
                        price_Label.SetText(Building.BuildingData.upgradePrice.ToString(CultureInfo.CurrentCulture));
                        
                        currentBudget_Label.SetText(Building.BuildingData.moneyPerSec.ToString());
                        
                        numberOfCustomers_Label?.SetText(Boot.container.Companies[0].numberOfCustomers.ToString());
                }
        }
}
