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
    public class Exit : AState
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
            this.companyData = companyData;

        }

        public override void OnStateEnter()
        {
            // before the application will be closed
            // the data need to be saved 
            cloud.SaveSettingsData();
            if (!Application.isPlaying)
            {
                Application.Quit();
            }
        }

        public override void OnStateUpdate()
        {
        }

        public override void OnStateExit()
        {
            
        }

        ~Exit()
        {
            
        }
    }
}