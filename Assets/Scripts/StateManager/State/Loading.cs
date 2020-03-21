using Enums;
using So_Template;
using SpawnManager;
using StateManager.State.Template;
using TMPro;
using UnityEngine;
using Zenject;

namespace StateManager.State
{
    public class Loading : AState
    {
        private TextMeshProUGUI label;

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
             label = GameObject.Find("Loading_Label").GetComponent<TextMeshProUGUI>();
        }

        public override void OnUpdate()
        {
            label.SetText($"Loading ... {sceneManager.SceneProgress}");
        }

        public override void OnExit()
        {
        }

        ~Loading()
        {
            
        }
    }
}