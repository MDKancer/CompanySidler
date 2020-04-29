using AudioManager;
using Enums;
using InputManager;
using So_Template;
using SpawnManager;
using StateManager.States.GameStates.Template;
using UnityEngine;
using VideoManager;
using Zenject;

namespace StateManager.States.GameStates
{
    public class PreGame : AState
    {
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
            
        }

        public override void OnStateEnter()
        {
            runTimeStateController.CurrentState = RunTimeState.NONE;
            //Debug.Log($"Current State {this}");
        }

        public override void OnStateUpdate()
        {
        }

        public override void OnStateExit()
        {
        }

        ~PreGame()
        {
            
        }
    }
}