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
    public class Intro : AState
    {
        //TODO: need to implement the OnCompleted Event
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
        }
        public override void OnStateEnter()
        {
            cloud.LoadAllResources();
            audioController.SetImportData();
            videoController.SetImportData();
            
            sceneManager.GoTo(Scenes.MAIN_MENU);
        }

        public override void OnStateUpdate()
        {
            //Debug.Log($"Current State On Update {this}");
        }

        public override void OnStateExit()
        {
            //Debug.Log($"Current State On Exit {this}");
        }

        ~Intro()
        {
            
        }
    }
}