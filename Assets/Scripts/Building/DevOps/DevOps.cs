using System.Collections;
using System.Collections.Generic;
using BootManager;
using BuildingPackage.OfficeWorker;
using Enums;
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
                prefab =  this.officePrefab,
                name = name,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                price = 0,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = 12,

                AvailableWorker = new List<BuildingWorkers<Employee, EntityType>>
                {
                    new BuildingWorkers<Employee, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Employee, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ADMIN),
                    new BuildingWorkers<Employee, EntityType>(EntityType.TESTER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.TEAM_LEADER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ANALYST),
                    new BuildingWorkers<Employee, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Employee, EntityType>(EntityType.AZUBI)
                    //TODO : Die Liste Erweitern / Ändern
                }

            };
            stateController.CurrentState = BuildingState.EMPTY;
        }
        public int Repair()
        {
            return BuildingData.wastage; 
        }

        public override void SwitchWorkingState()
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
        
        public override bool IsBuying
        {
            get => isBuying;
            set
            {
                Buy(buildingData.prefab, this.transform.position);
                isBuying = value;
            }
        }
        private IEnumerator UpdateManyGenerator()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                while (stateController.CurrentState == BuildingState.WORK)
                {
                    if (company != null)
                    {
                        company.CurrentBudget += Repair();
                        budget += Repair();
                    }
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}