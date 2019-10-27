using System.Collections;
using System.Collections.Generic;
using BuildingPackage.OfficeWorker;
using Constants;
using Human;
using UIPackage;
using UnityEngine;

namespace BuildingPackage
{
    public class DevOps : Building, iDevOps
    {

        void Awake()
        {
            this.budget = 0;
            buildingData = new BuildingData
            {
                buildingType = BuildingType.DEV_OPS,
                name = name,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = 12,

                AccessibleWorker = new List<BuildingWorkers<Worker, EntityType>>
                {
                    new BuildingWorkers<Worker, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Worker, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Worker, EntityType>(EntityType.ADMIN),
                    new BuildingWorkers<Worker, EntityType>(EntityType.TESTER),
                    new BuildingWorkers<Worker, EntityType>(EntityType.TEAM_LEADER),
                    new BuildingWorkers<Worker, EntityType>(EntityType.ANALYST),
                    new BuildingWorkers<Worker, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Worker, EntityType>(EntityType.AZUBI)
                    //TODO : Die Liste Erweitern / Ändern
                }

            };
            stateController.CurrentState = BuildingState.EMPTY;
        }

        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
        }


        public int Repair()
        {
            return BuildingData.wastage; 
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
        private IEnumerator UpdateManyGenerator()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                while (stateController.CurrentState == BuildingState.WORK)
                {
                    budget += Repair();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}