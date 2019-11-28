﻿using System;
using System.Globalization;
using BootManager;
using BuildingPackage;
using Enums;
using Human;
using InputManager;
using TMPro;
using UIPackage.UIBuildingContent;
using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    public static PlayerViewController playerViewController;
    public GameObject buildingInfo;
    private UIData uiData = new UIData();
    private GameObject buildingContentObj;
    
    private TextMeshProUGUI budget_Label;
    private TextMeshProUGUI numberOfCustomers_Label;
    private TextMeshProUGUI workersCount_Label;
    private TextMeshProUGUI buildingTitle_Label;
    private TextMeshProUGUI employeeCount_Label;
    private TextMeshProUGUI employeeLimit_Label;
    private TextMeshProUGUI price_Label;
    private TextMeshProUGUI currentBudget_Label;
    private GameObject curentHitPoints;
    public  GameState currentGameState;
    private int workerCount = 0;
    private bool haveContentBtn = false;
    private BuildingContent buildingContent;
    public void Awake()
    {
        playerViewController = this;
        //if(Boot.gameStateController.CurrentState == GameState.GAME)
        //{
            
            buildingInfo = GameObject.Find("BuildingInfo")?.gameObject;
            buildingContentObj = GameObject.Find("BuildingContent")?.gameObject;
            budget_Label = GameObject.Find("Panel_Geld_Nr")?.GetComponent<TextMeshProUGUI>();
            numberOfCustomers_Label = GameObject.Find("Panel_Kunde_Nr")?.GetComponent<TextMeshProUGUI>();
            workersCount_Label = GameObject.Find("Panel_Mitarbeiter_Nr")?.GetComponent<TextMeshProUGUI>();
                                        
            buildingTitle_Label = GameObject.Find("BuildingTitle")?.GetComponent<TextMeshProUGUI>();
            employeeCount_Label = GameObject.Find("WorkerCount")?.GetComponent<TextMeshProUGUI>();
            employeeLimit_Label = GameObject.Find("WorkerLimit")?.GetComponent<TextMeshProUGUI>();
            price_Label = GameObject.Find("Price")?.GetComponent<TextMeshProUGUI>();
            currentBudget_Label = GameObject.Find("Geld")?.GetComponent<TextMeshProUGUI>();
            curentHitPoints = GameObject.Find("HitPoints");
                                        
            buildingInfo.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        CurrentBudget();
        ShowBuildingInfoWindow();
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
                        if (Boot.runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO && Boot.gameStateController.CurrentState == GameState.GAME)
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
                    if (Boot.runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO && Boot.gameStateController.CurrentState == GameState.GAME)
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
                    else if(Boot.runtimeStateController.CurrentState != RunTimeState.BUILDING_INFO
                            && Boot.runtimeStateController.CurrentState != RunTimeState.GAME_MENU
                            && Boot.gameStateController.CurrentState == GameState.GAME)
                    {
                            RemoveBuildingContent();
                            buildingInfo?.SetActive(false);
                    }
            }
            private void RemoveBuildingContent()
            {
                uiData.RemoveAll();

                haveContentBtn = false;
            }
            private void CurrentBudget()
            {
                if(Boot.gameStateController.CurrentState == GameState.GAME)
                {
                                
                    budget_Label?.SetText(Boot.container.Companies[0].CurrentBudget.ToString());
                }
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
}
