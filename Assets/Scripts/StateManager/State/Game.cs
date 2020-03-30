using AudioManager;
using Entity.Customer;
using Enums;
using InputManager;
using So_Template;
using SpawnManager;
using StateManager.State.Template;
using UIDispatcher;
using UIDispatcher.GameComponents;
using UnityEngine;
using VideoManager;
using Zenject;

namespace StateManager.State
{
    public class Game : AState
    {
        private CustomerGenerator customerGenerator;
        public override void Init(SignalBus signalBus,
            Container.Cloud cloud,
            StateController<RunTimeState> runTimeStateController,
            InputController inputController,
            AudioController audioController,
            VideoController videoController,
            MonoBehaviour monoBehaviour,
            SceneManager.SceneManager sceneManager,
            SpawnController spawnController,
            CompanyData companyData)
        {
            this.signalBus = signalBus;
            this.cloud = cloud;
            this.runTimeStateController = runTimeStateController;
            this.inputController = inputController;
            this.audioController = audioController;
            this.videoController = videoController;
            this.sceneManager = sceneManager;
            this.spawnController = spawnController;
            this.monoBehaviour = monoBehaviour;
            this.companyData = companyData;
            this.customerGenerator = new CustomerGenerator();
        }

        public override void OnEnter()
        {
            // set the Container data and the Company Data
             cloud.SetDatas();
             spawnController.InitialSpawnWave();
             runTimeStateController.CurrentState = RunTimeState.PLAYING;
             
             //important data send on
             customerGenerator.Init(signalBus,cloud,spawnController,monoBehaviour);
            
             
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