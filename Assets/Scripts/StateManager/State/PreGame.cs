using Enums;
using So_Template;
using SpawnManager;
using StateManager.State.Template;
using UnityEngine;
using Zenject;

namespace StateManager.State
{
    public class PreGame : AState
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
            
        }

        public override void OnEnter()
        {
            runTimeStateController.CurrentState = RunTimeState.NONE;
            //Debug.Log($"Current State {this}");
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
        }

        ~PreGame()
        {
            
        }
    }
}