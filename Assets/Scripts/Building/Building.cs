using Constants;
using Life;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class Building : MonoBehaviour,iBuilding
    {
        public int money;
        
        protected float time;
        protected BuildingState buildingState;
        protected StateController<BuildingState> stateController = new StateController<BuildingState>();
        protected BuildingData buildingData;
        public void Upgrade()
        {
            buildingData.workPlacesLimit += 5;
            buildingData.upgradePrice *= 2;
            buildingData.moneyPerSec *= 2;
        }

        public void DoDamage(int damagePercent = 0)
        {
            buildingData.currenHhitPoints -= damagePercent;
        }

        public void Work()
        {
            throw new System.NotImplementedException();
        }

        public void SwitchWorkingState()
        {
          
        }

        public void ApplyWorker(Worker worker)
        {
            foreach (var VARIABLE in BuildingData.accesibleWorker)
            {
                if (VARIABLE.WorkerType == worker.HumanData.GetEntityType && VARIABLE.Worker == null)
                {
                    VARIABLE.Worker = worker;
                    buildingData.workers++;
                    return;
                }
            }
        }

        public void QuitWorker(Worker worker)
        {
            // TODO: das wird nach dem EntityPackage geändert.
            foreach (var VARIABLE in BuildingData.accesibleWorker)
            {
                if (VARIABLE.WorkerType == EntityType.TEAMLEADER && VARIABLE.Worker != null)
                {
                    VARIABLE.Worker = null;
                    buildingData.workers--;
                    return;
                }
            }
        }

        public bool BuildingRepair()
        {
            throw new System.NotImplementedException();
        }

        public BuildingData BuildingData { get => buildingData; }
        public BuildingState buildingWorkingState { get=> stateController.CurrentState; }
    }
}