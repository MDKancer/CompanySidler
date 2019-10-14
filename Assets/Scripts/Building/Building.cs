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
            buildingData.currentHitPoints -= damagePercent;
        }

        public void Work()
        {
        }

        public void SwitchWorkingState()
        {
          
        }

        public void ApplyWorker(Worker worker)
        {
            foreach (var VARIABLE in BuildingData.AccessibleWorker)
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
            foreach (var VARIABLE in BuildingData.AccessibleWorker)
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
            return false;
        }

        

        public BuildingData BuildingData => buildingData;
        public BuildingState buildingWorkingState => stateController.CurrentState;
    }
}