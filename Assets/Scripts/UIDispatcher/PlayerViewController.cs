using System;
using System.Globalization;
using BuildingPackage;
using Enums;
using GameCloud;
using Human;
using NaughtyAttributes;
using ProjectPackage;
using SpawnManager;
using StateMachine;
using TMPro;
using UIPackage.UIBuildingContent;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace PlayerView
{
    /// <summary>
    /// Player View Controller verwaltet einen Teil der Benutzeroberfläche sowie die Interaktionen zwischen dieser Oberfläche und den zugrunde liegenden Daten.
    /// <remarks>View-Controller erleichtern auch Übergänge zwischen verschiedenen Teilen Ihrer Benutzeroberfläche.</remarks>
    /// </summary>
    public class PlayerViewController : MonoBehaviour
    {
        public static PlayerViewController playerViewController;
        public UIData uiData = new UIData();
        public  GameState currentGameState;

        protected int workerCount = 0;
        protected bool haveContentBtn = false;

        protected BuildingContent buildingContent;
        protected SignalBus signalBus;
        protected StateController<GameState> gameStateController;
        protected StateController<RunTimeState> runtimeStateController;
        protected SpawnController spawnController;
        protected Container container;
        
        [Inject]
        protected virtual void Init(
                            SignalBus signalBus, 
                            StateController<GameState> gameStateController,
                            StateController<RunTimeState> runtimeStateController,
                            SpawnController spawnController,
                            Container container
                            )
        {
            this.signalBus = signalBus;
            this.gameStateController = gameStateController;
            this.runtimeStateController = runtimeStateController;
            this.spawnController = spawnController;
            this.container = container;
            
            SetDatas();
            signalBus.Subscribe<ShowBuildingData>(StateDependency);
        }
        
        protected virtual void StateDependency(ShowBuildingData showBuildingData)
        {
            switch (gameStateController.CurrentState)
            {
                case GameState.NONE:
                    break;
                case GameState.INTRO:
                    break;
                case GameState.LOADING:
                    break;
                case GameState.MAIN_MENU:
                    break;
                case GameState.PREGAME:
                    break;
                case GameState.GAME:
                    ShowBuildingInfoWindow();
                    break;
                case GameState.EXIT:
                    break;
            }
        }
        protected virtual void SetDatas()
        {
            playerViewController = this;

            this.buildingContent = new BuildingContent(ref uiData);

            buildingContent.windowUpdateEvent += UpdateWindow;
            buildingContent.buyBuildingEvent += BuyBuilding;
            buildingContent.buildingUpgradeEvent += UpgradeBuilding;
            buildingContent.applyEmployeeEvent += ApplyEmployee;
            buildingContent.changeStateBuilding += ChangeBuildingState;
            buildingContent.quitEmployeeEvent += QuitEmployee;
            buildingContent.startProject += ApplyProject;
                
            uiData.buildingInfo.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(gameStateController.CurrentState == GameState.GAME)
            {
                CurrentBudget();
            }
        }
        
        private void ApplyEmployee(String employeeType)
        {

                var spawnPosition = new Vector3(4f, 1f, 2f);
                var humanData = new EmployeeData(
                    company: container.Companies[0],
                    prefab: container.GetPrefabsByType(EntityType.DEVELOPER)[0],
                    entityType: GetValue(employeeType),
                    hisOffice: Building.BuildingData.buildingType
                );

                spawnController.SpawnWorker(Building, humanData, spawnPosition);
                workerCount++;
                uiData.workersCount_Label.SetText(workerCount.ToString());
        }

        private void QuitEmployee(Employee employee)
        {

                Building.QuitWorker(employee);
        }

        private void ApplyProject(Project project)
        {
            Building.ApplyProject(project);
        }
        private void BuyBuilding()
        {
            if (runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO) // && Boot.gameStateController.CurrentState == GameState.GAME
            {
                if (Building.Company.CurrentBudget >= Building.BuildingData.price)
                {
                    var currentBudget = container.Companies[0].CurrentBudget;
                    //TODO : wenn mann es kauf wird es von budge abgezogen
                    Building.IsBuying = true;
                }
            }
        }

        private void UpgradeBuilding()
        {
            if (runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO ) // && Boot.gameStateController.CurrentState == GameState.GAME
            {
                if(Building.Company.CurrentBudget >= Building.BuildingData.upgradePrice)
                {
                    Building.Upgrade();
                    // content refresh
                    UpdateWindow();
                }
            }
        }
        private void ChangeBuildingState(Transform button)
        {
                if (runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO 
                    && 
                    gameStateController.CurrentState == GameState.GAME)
                {
                        Building.SwitchWorkingState();
                        
                        TextMeshProUGUI btnText = button.GetComponent<TextMeshProUGUI>();
                        btnText.SetText(
                                btnText.text.ToLower().Contains(BuildingState.PAUSE.ToString().ToLower()) ? 
                                BuildingState.WORK.ToString() : BuildingState.PAUSE.ToString()
                                        );
                } 
        }

        public void FocusedBuilding(Building focusedBuilding)
        {
            Building = focusedBuilding;
        }

        protected virtual void UpdateWindow()
        {
            ShowBuildingInfoWindow();
        }
        private void ShowBuildingInfoWindow()
        {
            //________________________________________________________________________________________________________________________________________
                if (runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO 
                    && 
                    gameStateController.CurrentState == GameState.GAME)
                {
                        uiData.buildingInfo.SetActive(true);
                        if(Building != null)
                        {
                            SetBuildingDataInContent();
                        }
                        if (Building.IsBuying)
                        {
                            uiData.upgradeBtn.gameObject.SetActive(true);
                            uiData.buyBtn.gameObject.SetActive(false);
                        }
                        else
                        {
                            uiData.upgradeBtn.gameObject.SetActive(false);
                            uiData.buyBtn.gameObject.SetActive(true);
                        }
                        
                        float currentSize = (Building.BuildingData.currentHitPoints * 100f /
                                            Building.BuildingData.maxHitPoints) / 100f;
                        
                        uiData.currentHP.transform.localScale = new Vector3( currentSize, 1f,1f);
                        if(Building != null && Building.IsBuying)
                        {
                            if (!haveContentBtn)
                            {
                                    GenerateBuildingContent();
                            }
                        }
                }
                else if(runtimeStateController.CurrentState != RunTimeState.BUILDING_INFO
                        && runtimeStateController.CurrentState != RunTimeState.GAME_MENU
                        && gameStateController.CurrentState == GameState.GAME)
                {
                    if(Building != null && Building.IsBuying)
                    {
                        RemoveBuildingContent();
                    }
                    
                    uiData.buildingInfo?.SetActive(false);
                }
        }
        private void RemoveBuildingContent()
        {
            uiData.RemoveAll();

            haveContentBtn = false;
        }
        private void CurrentBudget()
        {
            if(gameStateController.CurrentState == GameState.GAME)
            {
                            
                uiData.budget_Label?.SetText(container.Companies[0].CurrentBudget.ToString());
            }
        }

        private EntityType GetValue(String value) => (EntityType) Enum.Parse(typeof(EntityType), value.ToUpper());
            

        private void SetBuildingDataInContent()
        {
                    
            uiData.buildingTitle_Label.SetText(Building.BuildingData.name);
                    
            uiData.employeeCount_Label.SetText(Building.BuildingData.workers.ToString());
                    
            uiData.employeeLimit_Label.SetText("/ "+Building.BuildingData.workPlacesLimit);
                    
            uiData.price_Label.SetText(Building.BuildingData.upgradePrice.ToString(CultureInfo.CurrentCulture));
                    
            uiData.currentBudget_Label.SetText(Building.BuildingData.moneyPerSec.ToString());
                    
            uiData.numberOfCustomers_Label?.SetText(container.Companies[0].numberOfCustomers.ToString());
        }
        private Building Building { get; set; }

        /// <summary>
        /// Es ist nur temporär um zu wissen ob alles gut Läuft.
        /// </summary>
        private void CheckCurrentState()
        {
            if(gameStateController != null)
            {
                if (currentGameState != gameStateController.CurrentState)
                {
                    currentGameState = gameStateController.CurrentState;
                }
            }
        }

        private void GenerateBuildingContent()
        {
            
            buildingContent.CreateBuildingContent(Building);

            haveContentBtn = true;
        }

        private void OnApplicationQuit()
        {
            signalBus.TryUnsubscribe<ShowBuildingData>(StateDependency);
        }
        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<ShowBuildingData>(StateDependency);
        }        
    }
}