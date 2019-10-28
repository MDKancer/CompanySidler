using System;
using System.Collections.Generic;
using System.Linq;
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
using UnityEngine.UI;

namespace UIPackage
{
        /// <summary>
        /// UIDispatcher is a Singleton.
        /// </summary>
        public class UIDispatcher : MonoBehaviour
        {
                public static UIDispatcher uiDispatcher;
                public  GameState currentGameState;
                public static int currentBudget;
                [Required]
                public GameObject buildingInfo;
                
                private Dictionary<Button,TextMeshProUGUI> buildContentBtn = new Dictionary<Button, TextMeshProUGUI>();
                private CreditsManager creditsManager;
                private GameObject creditPanel;
                private int workerCount = 0;
                private bool haveContentBtn = false;
                private BuildingContent buildingContent;

                private void Awake()
                {
                        if (uiDispatcher == null)
                        {
                                uiDispatcher = this;
                                creditsManager = GetComponent<CreditsManager>();
                                buildingInfo = GameObject.Find("BuildingInfo");
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
                        TextMeshProUGUI workersCount = GameObject.Find("Panel_Mitarbeiter_Nr").GetComponent<TextMeshProUGUI>();
                        workersCount.SetText(workerCount.ToString());
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
                                if(currentBudget >= Building.BuildingData.upgradePrice)
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
                                
                                GameObject.Find("BuildingTitle").GetComponent<TextMeshProUGUI>().SetText(Building.BuildingData.name);
                                GameObject.Find("WorkerCount").GetComponent<TextMeshProUGUI>().SetText(Building.BuildingData.workers.ToString());
                                GameObject.Find("WorkerLimit").GetComponent<TextMeshProUGUI>().SetText("/ "+Building.BuildingData.workPlacesLimit);
                                GameObject.Find("Price").GetComponent<TextMeshProUGUI>().SetText(Building.BuildingData.upgradePrice.ToString());
                                GameObject.Find("Geld").GetComponent<TextMeshProUGUI>().SetText(Building.BuildingData.moneyPerSec.ToString());
                                
                                
                                float currentSize = (Building.BuildingData.currentHitPoints * 100f /
                                                    Building.BuildingData.maxHitPoints) / 100f;
                                GameObject.Find("HitPoints").transform.localScale = new Vector3( currentSize, 1f,1f);
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
                        GameObject buildingContentObj = GameObject.Find("BuildingContent");

                        buildingContent = new BuildingContent(Building, buildingContentObj);
                        buildingContent.CreateBuildingContent(ref buildContentBtn);

                        haveContentBtn = true;
                }
                private void RemoveBuildingContent()
                {
                        for (int index = 0; index < buildContentBtn.Count; index++) {
                                var item = buildContentBtn.ElementAt(index);
                                var itemKey = item.Key;
                                var itemValue = item.Value;
                                buildContentBtn.Remove(itemKey);
                                Destroy(itemValue.gameObject);
                                Destroy(itemKey.gameObject);
                        }
                        haveContentBtn = false;
                }
                private void CurrentBudget()
                {
                       // if(Boot.gameStateController.CurrentState == GameState.GAME)
                        //{
                                TextMeshProUGUI money = GameObject.Find("Panel_Geld_Nr")?.GetComponent<TextMeshProUGUI>();
                                money?.SetText(Boot.container.Firmas[0].CurrentBudget.ToString());
                        //}
                }

                private EntityType GetValue(String value) => (EntityType) Enum.Parse(typeof(EntityType), value.ToUpper());
        }
}