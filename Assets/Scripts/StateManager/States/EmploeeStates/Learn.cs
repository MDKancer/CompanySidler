using AudioManager;
using Container;
using Enums;
using InputManager;
using So_Template;
using SpawnManager;
using StateManager.States.GameStates.Template;
using UnityEngine;
using VideoManager;
using Zenject;

namespace StateManager.States.EmploeeStates
{
    public class Learn : EmployeeState
    {
        public override void Init(SignalBus signalBus, Cloud cloud, StateController<RunTimeState> runTimeStateController, InputController inputController,
            AudioController audioController, VideoController videoController, MonoBehaviour monoBehaviour,
            SceneManager.SceneManager sceneManager, SpawnController spawnController, CompanyData companyData)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}