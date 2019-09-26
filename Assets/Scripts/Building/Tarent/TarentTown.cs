using System.Collections;
using System.Collections.Generic;
using BuildingPackage.Worker;
using Constants;
using JetBrains.Annotations;
using Life;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class TarentTown : MonoBehaviour,iBuilding, iTarent
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
                buildingType = BuildingType.TARENT_TOWN,
                name = name,
                maxHitPoints = 2000,
                currenHhitPoints = 2000,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = -8,
                
                accesibleWorker = new List<BuildingWorker<Human, EntityType>>
                {
                    new BuildingWorker<Human, EntityType>(EntityType.TEAMLEADER),
                    new BuildingWorker<Human, EntityType>(EntityType.TESTER),
                    new BuildingWorker<Human, EntityType>(EntityType.ANALYST),
                    new BuildingWorker<Human, EntityType>(EntityType.DEVELOPER),
                    new BuildingWorker<Human, EntityType>(EntityType.DEVELOPER),
                    new BuildingWorker<Human, EntityType>(EntityType.DESIGNER),
                    new BuildingWorker<Human, EntityType>(EntityType.DEVELOPER),
                    new BuildingWorker<Human, EntityType>(EntityType.AZUBI)
                    //TODO : Die Liste Erweitern
                }
            };
            
            stateController.CurrentState = BuildingState.EMPTY;
        }

        void Start()
        {
            
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
        }
        
        //coroutine -> alternative zu update

        public int ToHold()
        {
            return buildingData.workers * buildingData.moneyPerSec;
        }

        public void Upgrade()
        {
            buildingData.workPlacesLimit += 5;
            buildingData.upgradePrice *= 3;
            buildingData.moneyPerSec *= 3;
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

        /// <summary>
        /// geändert und erweitert
        /// </summary>
        /// <param name="worker"></param>
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

        public void QuitWorker([NotNull] Life.Worker worker)
        {
            // TODO: das wird nach dem EntityPackage geändert.
            foreach (var VARIABLE in BuildingData.accesibleWorker)
            {
                if (VARIABLE.WorkerType == EntityType.TEAMLEADER && VARIABLE.Worker != null)
                {
                    VARIABLE.Worker = null;
                    return;
                }
            }
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
                    money += ToHold();
                    UIDispatcher.currentBuget += ToHold();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}