using System;
using System.Globalization;
using BootManager;
using BuildingPackage;
using Enums;
using GameCloud;
using Human;
using InputManager;
using NaughtyAttributes;
using SpawnManager;
using StateMachine;
using TMPro;
using UIPackage.UIBuildingContent;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlayerView
{
    /// <summary>
    /// Player View Controller verwaltet einen Teil der Benutzeroberfläche sowie die Interaktionen zwischen dieser Oberfläche und den zugrunde liegenden Daten.
    /// <remarks>View-Controller erleichtern auch Übergänge zwischen verschiedenen Teilen Ihrer Benutzeroberfläche.</remarks>
    /// </summary>
    public class PlayerViewController : MonoBehaviour
    {
        public static PlayerViewController playerViewController;
        /// <summary>
        /// Das UI Fenster wo alle Informationen über Das Gebäude sich befinden.
        /// </summary>
        [Required]
        public GameObject buildingInfo;
        [ReadOnly]
        public  GameState currentGameState;
        

        
        //------------------Static UI Elements----------------
        private GameObject buildingContentObj;

        private Button upgradeBtn;
        private Button buyBtn;
        private TextMeshProUGUI budget_Label;
        private TextMeshProUGUI numberOfCustomers_Label;
        private TextMeshProUGUI workersCount_Label;
        private TextMeshProUGUI buildingTitle_Label;
        private TextMeshProUGUI employeeCount_Label;
        private TextMeshProUGUI employeeLimit_Label;
        private TextMeshProUGUI price_Label;
        private TextMeshProUGUI currentBudget_Label;
        /// <summary>
        /// Akktuelles  Gebäude-Lebenspunkte.
        /// </summary>
        private GameObject currentHP;
        //----------------------------------------------------
        
        //-----------------Generic UI Elements----------------
        private BuildingContent buildingContent;
        private UIData uiData = new UIData();
        //----------------------------------------------------
        private int workerCount = 0;
        private bool haveContentBtn = false;

        [Inject]
        private StateController<GameState> gameStateController;
        [Inject]
        private StateController<RunTimeState> runtimeStateController;
        [Inject]
        private SpawnController spawnController;
        [Inject]
        private Container container;
        public void Awake()
        {
            playerViewController = this;
            if(gameStateController.CurrentState == GameState.GAME)
            {
                
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
                currentHP = GameObject.Find("HitPoints");

                upgradeBtn = GameObject.Find("BuildingUpgrade").GetComponent<Button>();
                upgradeBtn.gameObject.SetActive(false);
                buyBtn = GameObject.Find("BuildingBuying").GetComponent<Button>();
                
                buildingInfo.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(gameStateController.CurrentState == GameState.GAME)
            {
                CurrentBudget();
                ShowBuildingInfoWindow();
            }
        }
        
        public void ApplyEmployee(String employeeType)
        {
            var  spawnPosition = new Vector3(4f,1f,2f);
            var humanData = new EmployeeData(GetValue(employeeType),Building.BuildingData.buildingType);

            spawnController.SpawnWorker(Building, humanData,spawnPosition);
            workerCount++;
            workersCount_Label.SetText(workerCount.ToString());
        }
        public void BuyBuilding()
        {
            if (runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO) // && Boot.gameStateController.CurrentState == GameState.GAME
            {
                if (Building.Company.CurrentBudget >= Building.BuildingData.price)
                {
                    var currentBudget = container.Companies[0].CurrentBudget;
                    Building.IsBuying = true;
                }
            }
        }

        public void UpgradeBuilding()
        {
            if (runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO ) // && Boot.gameStateController.CurrentState == GameState.GAME
            {
                if(Building.Company.CurrentBudget >= Building.BuildingData.upgradePrice)
                {
                    Building.Upgrade();
                    // content refresh
                    RemoveBuildingContent();
                }
            }
        }
        public void ChangeBuildingState(Transform button)
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
        private void ShowBuildingInfoWindow()
        {
            
            
            //________________________________________________________________________________________________________________________________________
                if (runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO 
                    && 
                    gameStateController.CurrentState == GameState.GAME)
                {
                        buildingInfo.SetActive(true);
                        if(Building != null)
                        {
                            SetBuildingDataInContent();
                        }
                        if (Building.IsBuying)
                        {
                            upgradeBtn.gameObject.SetActive(true);
                            buyBtn.gameObject.SetActive(false);
                        }
                        else
                        {
                            upgradeBtn.gameObject.SetActive(false);
                            buyBtn.gameObject.SetActive(true);
                        }
                        
                        float currentSize = (Building.BuildingData.currentHitPoints * 100f /
                                            Building.BuildingData.maxHitPoints) / 100f;
                        
                        currentHP.transform.localScale = new Vector3( currentSize, 1f,1f);
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
            if(gameStateController.CurrentState == GameState.GAME)
            {
                            
                budget_Label?.SetText(container.Companies[0].CurrentBudget.ToString());
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
                    
            numberOfCustomers_Label?.SetText(container.Companies[0].numberOfCustomers.ToString());
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
                buildingContent = new BuildingContent(buildingContentObj);
                buildingContent.CreateBuildingContent(ref uiData, Building);

            haveContentBtn = true;
        }
    }
}