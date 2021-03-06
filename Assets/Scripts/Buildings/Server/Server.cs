using System.Collections;
using System.Collections.Generic;
using Entity.Employee;
using Enums;
using UnityEngine;

namespace Buildings.Server
{
    public class Server : Building, iServer
    {
        void Awake()
        {
            buildingData = new BuildingData
            {
                buildingType = BuildingType.SERVER,
                prefab =  this.officePrefab,
                name = name,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                price = 0,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = 5,
                
                AvailableWorker = new List<BuildingWorkers<Employee, EntityType>>
                {
                    new BuildingWorkers<Employee, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Employee, EntityType>(EntityType.TESTER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ADMIN),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ADMIN),
                    new BuildingWorkers<Employee, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Employee, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ADMIN),
                    new BuildingWorkers<Employee, EntityType>(EntityType.AZUBI)
                    //TODO : Die Liste Erweitern / Ändern
                }
            };
            stateController.CurrentState = BuildingState.EMPTY;
        }
        public int Hosten()
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
                        company.CurrentBudget += Hosten();
                        budget += Hosten();
                    }
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}