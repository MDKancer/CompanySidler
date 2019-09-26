using System.Collections;
using Constants;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class Accounting : MonoBehaviour, iBuilding, iAccounting
    {
        public int money;
        
        private float time;
        private BuildingState buildingState;
        private StateController<BuildingState> stateController = new StateController<BuildingState>();
        private BuildingData buildingData;

        void Awake()
        {
            buildingData = new BuildingData
            {
                buildingType = BuildingType.ACCOUNTING,
                name = name,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = -3
            };
            stateController.CurrentState = BuildingState.EMPTY;
        }

        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
        }

        public int Compute()
        {
            return buildingData.workers * buildingData.moneyPerSec;
        }

        public void Upgrade()
        {
            buildingData.workPlacesLimit += 5;
        }

        public void DoDamage(int damagePercent = 0)
        {
            throw new System.NotImplementedException();
        }

        public void Work()
        {
            
        }

        public void SwitchWorkingState()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                stateController.CurrentState = BuildingState.PAUSE;
                StopCoroutine(UpdateManyGenerator());
            }
            else
            {
                stateController.CurrentState = BuildingState.WORK;
                StartCoroutine( UpdateManyGenerator());
            }
        }

        public void ApplyWorker(Life.Worker worker)
        {
            foreach (var VARIABLE in BuildingData.accesibleWorker)
            {
                if (VARIABLE.WorkerType == worker.HumanData.GetEntityType && VARIABLE.Worker == null)
                {
                    VARIABLE.Worker = worker;
                    return;
                }
            }
        }

        public void QuitWorker(Life.Worker worker)
        {
            throw new System.NotImplementedException();
        }

        public bool BuildingRepair()
        {
            throw new System.NotImplementedException();
        }

        public BuildingData BuildingData { get=> buildingData; set => buildingData = value; }
        public BuildingState buildingWorkingState { get=>stateController.CurrentState; }
        private IEnumerator UpdateManyGenerator()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                while (stateController.CurrentState == BuildingState.WORK)
                {
                    money += Compute();
                    UIDispatcher.currentBuget += Compute();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}