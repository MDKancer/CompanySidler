using Enums;
using GameCloud;
using Human.Customer.Generator;
using InputManager;
using PlayerView;
using SceneController;
using SpawnManager;
using UnityEngine;
using Zenject;

namespace StateMachine.States
{
    public class Game : AState
    {
        private InputController inputController;
        private CustomerGenerator customerGenerator;
        public override void Init(SignalBus signalBus,
            Container container,
            StateController<RunTimeState> runTimeStateController,
            MonoBehaviour monoBehaviour,
            SceneManager sceneManager,
            SpawnController spawnController,
            CompanyData companyData)
        {
            this.signalBus = signalBus;
            this.container = container;
            this.runTimeStateController = runTimeStateController;
            this.sceneManager = sceneManager;
            this.spawnController = spawnController;
            this.monoBehaviour = monoBehaviour;
            this.companyData = companyData;
            this.inputController = new InputController();
            this.customerGenerator = new CustomerGenerator();
        }

        public override void OnEnter()
        {
            // set the Container data and the Company Data
             container.SetDatas();
             spawnController.InitialSpawnWave();
             runTimeStateController.CurrentState = RunTimeState.PLAYING;
             
             //important data send on
             inputController.Init(signalBus,runTimeStateController,monoBehaviour,companyData);
             customerGenerator.Init(signalBus,container,spawnController,monoBehaviour);
            
             
             inputController.SetCameraController();
             inputController.showBuildingDataEvent += PlayerViewController.playerViewController.FocusedBuilding;
             
             customerGenerator.CreateCustomers();

             monoBehaviour.StartCoroutine(customerGenerator.CreateNewCostumer());
             //Debug.Log($"Current State {this}");
        }

        public override void OnUpdate()
        {
            inputController.CameraEvents();
            PlayerViewController.playerViewController.CurrentBudget();
        }

        public override void OnExit()
        {
        }

        ~Game()
        {
        }
    }
}