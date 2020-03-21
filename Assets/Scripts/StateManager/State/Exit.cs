using Enums;
using So_Template;
using SpawnManager;
using StateManager.State.Template;
using UnityEngine;
using Zenject;

namespace StateManager.State
{
    public class Exit : AState
    {
        public override void Init(SignalBus signalBus,
            Container.Cloud cloud,
            StateController<RunTimeState> runTimeStateController,
            MonoBehaviour monoBehaviour,
            SceneManager.SceneManager sceneManager,
            SpawnController spawnController,
            CompanyData companyData)
        {
            this.signalBus = signalBus;
            this.cloud = cloud;
            this.runTimeStateController = runTimeStateController;
            this.sceneManager = sceneManager;
            this.spawnController = spawnController;
            this.monoBehaviour = monoBehaviour;
            this.companyData = companyData;

        }

        public override void OnEnter()
        {
            Application.Quit();
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
            
        }

        ~Exit()
        {
            
        }
    }
}